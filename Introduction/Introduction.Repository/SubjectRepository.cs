using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Introduction.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        static string ConnectionString = "Host=localhost;Port=5432;Database=WebAPIUniversity;Username=postgres;Password=postgres";

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

        public async Task<List<Subject>?> GetAllSubjectInfoAsync()
        {
            try
            {
                List<Subject> subjects = new List<Subject>();

                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"SELECT * FROM subject";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var subject = new Subject
                        {
                            Id = Guid.Parse(reader[0].ToString()),
                            DepartmentId = Guid.Parse(reader[1].ToString()),
                            Name = reader["name"].ToString(),
                            TimeCreated = DateTime.Parse(reader["timecreated"].ToString())
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
