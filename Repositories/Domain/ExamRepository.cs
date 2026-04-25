using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class ExamRepository : BaseRepository<ExamTb>, IExamRepository
    {
        private readonly StudentdbContext _context;

        public ExamRepository(StudentdbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<ExamTb>> GetAllAsync()
        {
            return await _context.ExamTbs
                .Where(x => x.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<ExamTb> GetByIdAsync(int id)
        {
            return await _context.ExamTbs.FindAsync(id);
        }

        public async Task AddAsync(ExamTb exam)
        {
            await _context.ExamTbs.AddAsync(exam);
        }

        public void Update(ExamTb exam)
        {
            _context.ExamTbs.Update(exam);
        }

        public void Delete(ExamTb exam)
        {
            exam.IsDeleted = true;
            _context.ExamTbs.Update(exam);
        }
    
    }
}
