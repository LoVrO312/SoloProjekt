using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class SubjectService : ISubjectService
    {
        public async Task<bool> CreateSubjectAsync(Subject newSubject)
        {
            SubjectRepository repo = new SubjectRepository();
            
            newSubject.Id = Guid.NewGuid();
            newSubject.TimeCreated = DateTime.Now;

            return await repo.CreateSubjectAsync(newSubject);
        }

        public async Task<Subject?> GetSubjectInfoAsync(Guid id)
        {
            SubjectRepository repo = new SubjectRepository();
            return await repo.GetSubjectInfoAsync(id);
        }

        public async Task<List<Subject>?> GetAllSubjectInfoAsync()
        {
            SubjectRepository repo = new SubjectRepository();
            return await repo.GetAllSubjectInfoAsync();
        }

        public async Task<bool> ChangeSubjectDepartmentAsync(Guid id, Guid newDepartmentId)
        {
            SubjectRepository repo = new SubjectRepository();
            return await repo.ChangeSubjectDepartmentAsync(id, newDepartmentId);
        }

        public async Task<bool> RemoveSubjectAsync(Guid id)
        {
            SubjectRepository repo = new SubjectRepository();
            return await repo.RemoveSubjectAsync(id);
        }

    }
}
