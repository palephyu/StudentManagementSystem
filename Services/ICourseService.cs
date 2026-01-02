using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course_VM>> GetAll();
        Task <Course_VM> GetById(int id);
        Task Create(Course_VM vm);
        Task Update(Course_VM vm);
        Task Delete(int id);

    }
}
