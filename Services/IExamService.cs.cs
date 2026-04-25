using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public interface IExamService
    {
        Task<IEnumerable<Exam_VM>> GetAllAsync();
        Task<Exam_VM> GetByIdAsync(int id);
        Task CreateAsync(Exam_VM vm);
        Task UpdateAsync(Exam_VM vm);
        Task DeleteAsync(int id);
    }
}
