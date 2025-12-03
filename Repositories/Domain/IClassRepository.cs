using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Repositories.Domain
{
    public interface IClassRepository: IBaseRepository<ClassTb>
    {
        Task<IEnumerable<ClassTb>> GetAllClassesAsync();
    
    }
}
