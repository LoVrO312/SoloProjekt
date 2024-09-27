using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Introduction.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Xml.XPath;
using Introduction.WebAPI.Rest;
using Microsoft.AspNetCore.Identity;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("subject")]
    public class SubjectController : Controller
    {
        private ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSubjectAsync(CreateSubjectRest subject)
        {
            Subject newSubject = new Subject
            {
                DepartmentId = subject.DepartmentId,
                EctsPoints = subject.EctsPoints,
                Name = subject.Name,
                TimeCreated = subject.TimeCreated
            };

            if (await _service.CreateSubjectAsync(newSubject))
            {
                return Ok("Subject added successfully");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("read/{id}")]
        public async Task<IActionResult> GetSubjectInfoAsync(Guid id)
        {
            Subject? foundSubject = await _service.GetSubjectInfoAsync(id);

            if (foundSubject == null)
            {
                return BadRequest();
            }
            return Ok(foundSubject);
        }

        [HttpGet]
        [Route("readallfiltered")]
        public async Task<IActionResult> GetAllSubjectFilteredAsync(
            Guid? departmentId, int? minEctsPoints, int? maxEctsPoints, DateTime? fromTimeCreated,
            DateTime? toTimeCreated, int recordsPerPage = 15, int pageNumber = 1, string sortBy = "Subject Name", string sortOrder = "Descending", string searchQuery = "")
        {
        SubjectFilter filter = new SubjectFilter
                    {
                        SearchQuery = searchQuery,
                        DepartmentId = departmentId,
                        MinEctsPoints = minEctsPoints,
                        MaxEctsPoints = maxEctsPoints,
                        FromTimeCreated = fromTimeCreated,
                        ToTimeCreated = toTimeCreated
                    };
        Paging paging = new Paging
                    {
                        RecordsPerPage = recordsPerPage,
                        PageNumber = pageNumber
                    };
        Sorting sorting = new Sorting
                    {
                        SortBy = sortBy,
                        SortOrder = sortOrder
                    };

        List<Subject>? foundSubjects = await _service.GetAllSubjectFilteredAsync(filter, paging, sorting);

        if (foundSubjects == null)
        {
            return BadRequest("No results");
        }
        return Ok(foundSubjects);
        }

        [HttpGet]
        [Route("readcorrespondingdepartments")]
        public async Task<IActionResult> GetSubjectDepartmentsAsync()
        {
            List<Department>? departments = await _service.GetSubjectDepartmentsAsync();
            if (departments == null)
            {
                return BadRequest();
            }

            List<DepartmentRest> departmentsRest = departments.Select(dpt => new DepartmentRest
                    {
                        Id = dpt.Id,
                        Name = dpt.Name,
                        Subjects = dpt.Subjects
                    }).ToList();

            return Ok(departmentsRest);
        }


        [HttpPut]
        [Route("update/{id}/{newDepartmentId}")]
        public async Task<IActionResult> ChangeSubjectDepartmentAsync([Required] Guid id, Guid newDepartmentId)
        {
            if (await _service.ChangeSubjectDepartmentAsync(id, newDepartmentId))
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> RemoveSubjectAsync(Guid id)
        {
            if (await _service.RemoveSubjectAsync(id))
            {
                return Ok();
            }
            return BadRequest();
        }



    }
}
