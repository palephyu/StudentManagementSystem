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
    }
}
