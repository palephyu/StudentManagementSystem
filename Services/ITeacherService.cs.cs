using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services

{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher_VM>> GetAll();
        Task Create(Teacher_VM vm);
    }
}
