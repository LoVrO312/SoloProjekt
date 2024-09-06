using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : Controller
    {
        static string ConnectionString = "Host=localhost;Port=5432;Database=WebAPIUniversity;Username=postgres;Password=postgres";

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateSubject(Subject newSubject)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"INSERT INTO subject VALUES(@id,@departmentId,@name,@timeCreated);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, newSubject.Id);
                command.Parameters.AddWithValue("@departmentId", NpgsqlTypes.NpgsqlDbType.Uuid, newSubject.DepartmentId);
                command.Parameters.AddWithValue("@name", newSubject.Name);
                // makni timezonu kada saljes datetime (Z sa kraja stringa u JSON-u)
                command.Parameters.AddWithValue("@timeCreated", NpgsqlTypes.NpgsqlDbType.Timestamp, newSubject.TimeCreated);

                connection.Open();
                var NumberOfCommits = command.ExecuteNonQuery(); // number of rows affected
                connection.Close();

                if (NumberOfCommits == 0)
                {
                    return BadRequest();
                }
                return Ok("Subject added successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Read/{id}")]
        public IActionResult GetSubjectInfo(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"SELECT * FROM subject WHERE id = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    var subject = new Subject();

                    reader.Read();

                    subject.Id = Guid.Parse(reader[0].ToString());
                    subject.DepartmentId = Guid.Parse(reader[1].ToString());
                    subject.Name = reader["name"].ToString();
                    subject.TimeCreated = DateTime.Parse(reader["timecreated"].ToString());
                    return Ok(subject);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ReadAll")]
        public IActionResult GetAllSubjectInfo(Guid id)
        {
            try
            {
                List<Subject> subjects = new List<Subject>();
                
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"SELECT * FROM subject";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    var subject = new Subject();

                    while (reader.Read())
                    {
                        subject.Id = Guid.Parse(reader[0].ToString());
                        subject.DepartmentId = Guid.Parse(reader[1].ToString());
                        subject.Name = reader["name"].ToString();
                        subject.TimeCreated = DateTime.Parse(reader["timecreated"].ToString());

                        subjects.Add(subject);
                    }
                    return Ok(subjects);
                }   
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult ChangeSubjectDepartment([Required] Guid id, [FromBody] Guid newDepartmentId)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"UPDATE subject SET departmentid = @newDepartmentId WHERE id = @id";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@newDepartmentId", newDepartmentId);
                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                connection.Open();
                int NumberOfCommits = command.ExecuteNonQuery();
                connection.Close();

                if (NumberOfCommits == 0)
                {
                    return BadRequest();
                }
                return Ok("Department updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult RemoveSubject(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                string commandText = $"DELETE FROM \"subject\" WHERE \"id\" = @id; ";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id",id);

                connection.Open();
                var NumberOfCommits = command.ExecuteNonQuery(); 
                connection.Close();

                if (NumberOfCommits == 0)
                    return NotFound();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
