using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class TeacherRepository : BaseRepository<TeacherTb>, ITeacherRepository
    {
        private readonly StudentdbContext _studentdbContext;
        public TeacherRepository(StudentdbContext context) : base(context)
        {
            _studentdbContext = context;
        }
    }
}
