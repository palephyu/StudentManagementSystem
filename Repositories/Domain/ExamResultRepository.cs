using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class ExamResultRepository : BaseRepository<ExamResultTb>, IExamResultRepository
    {
        private readonly StudentdbContext _context;

        public ExamResultRepository(StudentdbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
