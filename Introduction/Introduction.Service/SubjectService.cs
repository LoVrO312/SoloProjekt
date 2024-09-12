using Introduction.Common;
using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class SubjectService : ISubjectService
    {
        private ISubjectRepository _repository;

        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateSubjectAsync(Subject newSubject)
        {   
            newSubject.Id = Guid.NewGuid();
            newSubject.TimeCreated = DateTime.Now;

            return await _repository.CreateSubjectAsync(newSubject);
        }

        public async Task<Subject?> GetSubjectInfoAsync(Guid id)
        {
            return await _repository.GetSubjectInfoAsync(id);
        }

        public async Task<List<Subject>?> GetAllSubjectFilteredAsync(SubjectFilter filter, Paging paging, Sorting sorting)
        {
            return await _repository.GetAllSubjectFilteredAsync(filter, paging, sorting);
        }

        public async Task<List<Department>?> GetSubjectDepartmentsAsync()
        {
            return await _repository.GetSubjectDepartmentsAsync();
        }

        public async Task<bool> ChangeSubjectDepartmentAsync(Guid id, Guid newDepartmentId)
        {
            return await _repository.ChangeSubjectDepartmentAsync(id, newDepartmentId);
        }

        public async Task<bool> RemoveSubjectAsync(Guid id)
        {
            return await _repository.RemoveSubjectAsync(id);
        }

    }
}
