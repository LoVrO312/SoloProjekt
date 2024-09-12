using Introduction.Common;
using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface ISubjectService
    {
        Task<bool> CreateSubjectAsync(Subject newSubject);
        Task<Subject?> GetSubjectInfoAsync(Guid id);
        Task<List<Subject>?> GetAllSubjectFilteredAsync(SubjectFilter filter, Paging paging, Sorting sorting);
        Task<List<Department>?> GetSubjectDepartmentsAsync();
        Task<bool> ChangeSubjectDepartmentAsync(Guid id, Guid newDepartmentId);
        Task<bool> RemoveSubjectAsync(Guid id);
    }
}
