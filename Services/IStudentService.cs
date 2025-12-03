using StudentManagementSystem.ViewModels;
using System.Linq.Expressions;

namespace StudentManagementSystem.Services
{
    public interface IStudentService
    {       
        Task<IEnumerable<Student_VM>> GetAllAsync();
        Task<Student_VM> GetByIdAsync(int id);
        Task CreateAsync(Student_VM vm);
        Task UpdateAsync(Student_VM vm);
        Task DeleteAsync(int id);
    }
}
