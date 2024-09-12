using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;

namespace Introduction.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        static string ConnectionString = "Host=localhost;Port=5432;Database=WebAPIUniversity;Username=postgres;Password=postgres";
        static Dictionary<string, string> MapColumnNames = new Dictionary<string, string>()
                {
                    { "Subject name", "s.name" },
                    { "Department name", "d.name" },
                    { "Number of ECTS", "s.ectspoints" },
                    { "Time created", "s.timecreated" }
                };
        public async Task<bool> CreateSubjectAsync(Subject newSubject)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"INSERT INTO subject VALUES(@id,@departmentId,@name,@timeCreated);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, newSubject.Id);
                command.Parameters.AddWithValue("@departmentId", NpgsqlTypes.NpgsqlDbType.Uuid, newSubject.DepartmentId);
                command.Parameters.AddWithValue("@name", newSubject.Name);
                // incorrect syntax : 2024-09-09T09:01:32.293Z", correct syntax : 2024-09-09T09:01:32"
                command.Parameters.AddWithValue("@timeCreated", NpgsqlTypes.NpgsqlDbType.Timestamp, newSubject.TimeCreated);

                connection.Open();
                int numberOfCommits = await command.ExecuteNonQueryAsync(); // number of rows affected
                connection.Close();

                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Subject?> GetSubjectInfoAsync(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"SELECT * FROM subject WHERE id = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    var subject = new Subject();

                    reader.Read();

                    subject.Id = Guid.Parse(reader[0].ToString());
                    subject.DepartmentId = Guid.Parse(reader[1].ToString());
                    subject.Name = reader["name"].ToString();
                    subject.TimeCreated = DateTime.Parse(reader["timecreated"].ToString());
                    return subject;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Subject>?> GetAllSubjectFilteredAsync(SubjectFilter filter, Paging paging, Sorting sorting)
        {
            try
            {
                List<Subject> subjects = new List<Subject>();

                var commandText = new StringBuilder("SELECT s.id as sid, s.name as sname, s.ectspoints as sectspoints, s.timecreated as stimecreated, d.id as did, d.name as dname FROM subject s JOIN department d ON s.departmentid = d.id WHERE 1=1");

                if (!String.IsNullOrEmpty(filter.SearchQuery))
                {
                    commandText.Append($" AND (s.name LIKE '%' || @searchQuery || '%' OR d.name LIKE '%' || @searchQuery || '%')");
                }
                if (filter.DepartmentId != null)
                {
                    commandText.Append($" AND d.id = @departmentId");
                }
                if (filter.MinEctsPoints != null)
                {
                    commandText.Append($" AND s.ectspoints >= @minEctsPoints");
                }
                if (filter.MaxEctsPoints != null)
                {
                    commandText.Append($" AND s.ectspoints <= @maxEctsPoints");
                }
                if (filter.FromTimeCreated != null)
                {
                    commandText.Append($" AND s.timecreated >= @fromTimeCreated");
                }
                if (filter.ToTimeCreated != null)
                {
                    commandText.Append($" AND s.timecreated <= @toTimeCreated");
                }

                // prevention of injection attacks and constraint checks
                string sortBy = MapColumnNames.ContainsKey(sorting.SortBy) ? MapColumnNames[sorting.SortBy] : "s.name";
                string sortOrder = sorting.SortOrder == "Descending" ? "DESC" : "ASC";
                int pageNumber = paging.PageNumber < 1 ? 1 : paging.PageNumber;
                int recordsPerPage = (paging.RecordsPerPage < 1 || paging.RecordsPerPage > 15) ? 15 : paging.RecordsPerPage;

                commandText.Append($" ORDER BY {sortBy} {sortOrder}");
                commandText.Append($" OFFSET @offset LIMIT @recordsPerPage;");

                using var connection = new NpgsqlConnection(ConnectionString);
                using var command = new NpgsqlCommand(commandText.ToString(), connection);

                if (!String.IsNullOrEmpty(filter.SearchQuery))
                {
                    command.Parameters.AddWithValue("@searchQuery", filter.SearchQuery);
                }
                if (filter.DepartmentId != null)
                {
                    command.Parameters.AddWithValue("@departmentId", NpgsqlTypes.NpgsqlDbType.Uuid, filter.DepartmentId);
                }
                if (filter.MinEctsPoints != null)
                {
                    command.Parameters.AddWithValue("@minEctsPoints", filter.MinEctsPoints);
                }
                if (filter.MaxEctsPoints != null)
                {
                    command.Parameters.AddWithValue("@maxEctsPoints", filter.MaxEctsPoints);
                }
                if (filter.FromTimeCreated != null)
                {
                    command.Parameters.AddWithValue("@fromTimeCreated", NpgsqlTypes.NpgsqlDbType.Timestamp, filter.FromTimeCreated);
                }
                if (filter.ToTimeCreated != null)
                {
                    command.Parameters.AddWithValue("@toTimeCreated", NpgsqlTypes.NpgsqlDbType.Timestamp, filter.ToTimeCreated);
                }
                command.Parameters.AddWithValue("@offset", (pageNumber - 1) * recordsPerPage);
                command.Parameters.AddWithValue("@recordsPerPage", recordsPerPage);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                // parsing the response from the database
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Subject subject = new Subject
                        {
                            Id = Guid.Parse(reader["sid"].ToString()),
                            Name = reader["sname"].ToString(),
                            TimeCreated = DateTime.Parse(reader["stimecreated"].ToString()),
                            EctsPoints = Convert.ToInt32(reader["sectspoints"]),
                            Department = new Department
                            {
                                Id = Guid.Parse(reader["did"].ToString()),
                                Name = reader["dname"].ToString()
                            }
                        };
                        subjects.Add(subject);
                    }
                    return subjects;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Department>?> GetSubjectDepartmentsAsync()
        {
            try
            {
                List<Department> departments = new List<Department>();

                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"SELECT s.name as sname, d.name as dname, d.id as did FROM subject s JOIN department d ON s.departmentid = d.id;";
                using var command = new NpgsqlCommand(commandText, connection);
                
                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                
                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        Guid departmentId = Guid.Parse(reader["did"].ToString());

                        Department? department = departments.FirstOrDefault(d => d.Id == departmentId);

                        if (department == null)
                        {
                            department = new Department
                            {
                                Id = departmentId,
                                Name = reader["dname"].ToString(),
                                Subjects = new List<Subject>()
                            };

                            departments.Add(department);
                        }

                        department.Subjects.Add(new Subject { Name = reader["sname"].ToString() });
                    }
                    return departments;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeSubjectDepartmentAsync(Guid id, Guid newDepartmentId)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"UPDATE subject SET departmentid = @newDepartmentId WHERE id = @id";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@newDepartmentId", newDepartmentId);
                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                connection.Open();
                int numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveSubjectAsync(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"DELETE FROM \"subject\" WHERE \"id\" = @id; ";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
