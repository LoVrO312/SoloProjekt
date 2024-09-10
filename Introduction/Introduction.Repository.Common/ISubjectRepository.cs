using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository.Common
{
    public interface ISubjectRepository
    {
        Task<bool> CreateSubjectAsync(Subject newSubject);
        Task<Subject?> GetSubjectInfoAsync(Guid id);
        Task<List<Subject>?> GetAllSubjectInfoAsync();
        Task<bool> ChangeSubjectDepartmentAsync(Guid id, Guid newDepartmentId);
        Task<bool> RemoveSubjectAsync(Guid id);
    }
}
