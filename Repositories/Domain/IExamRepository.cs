using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;

namespace StudentManagementSystem.Repositories.Domain
{
    public interface IExamRepository : IBaseRepository<ExamTb>
    {
        Task<IEnumerable<ExamTb>> GetAllAsync();
        Task<ExamTb> GetByIdAsync(int id);
        Task AddAsync(ExamTb exam);
        void Update(ExamTb exam);
        void Delete(ExamTb exam);
    }
}
