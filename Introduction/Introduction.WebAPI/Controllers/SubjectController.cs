using Introduction.Model;
using Introduction.Service;
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
        public async Task<IActionResult> CreateSubjectAsync(Subject newSubject)
        {
            SubjectService service = new SubjectService();

            if (await service.CreateSubjectAsync(newSubject))
            {
                return Ok("Subject added successfully");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Read/{id}")]
        public async Task<IActionResult> GetSubjectInfoAsync(Guid id)
        {
            SubjectService service = new SubjectService();
            Subject? foundSubject = await service.GetSubjectInfoAsync(id);

            if (foundSubject == null)
            {
                return BadRequest();
            }
            return Ok(foundSubject);
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<IActionResult> GetAllSubjectInfoAsync()
        {
            SubjectService service = new SubjectService();
            List<Subject>? foundSubjects = await service.GetAllSubjectInfoAsync();

            if (foundSubjects == null)
            {
                return BadRequest();
            }
            return Ok(foundSubjects);
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> ChangeSubjectDepartmentAsync([Required] Guid id, [FromBody] Guid newDepartmentId)
        {
            SubjectService service = new SubjectService();

            if (await service.ChangeSubjectDepartmentAsync(id, newDepartmentId))
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> RemoveSubjectAsync(Guid id)
        {
            SubjectService service = new SubjectService();

            if (await service.RemoveSubjectAsync(id))
            {
                return Ok();
            }
            return BadRequest();
        }



    }
}
