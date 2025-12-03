using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Repositories.Domain
{
    public interface IStudentRepository : IBaseRepository<StudentTb>
    {
        IEnumerable<Student_VM> GetStudents();      
        IEnumerable<Student_VM> GetSteudentByStudentId(string StudentId);
        Task<Student_VM> GetStudentWithClassAsync(int id);
        Task<bool> IsAlreadyExist(string StudentId, string FullName);
        Task<string?> GetLastStudent();  
       

    }
}
