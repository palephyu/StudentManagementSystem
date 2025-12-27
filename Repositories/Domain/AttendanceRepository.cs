using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class AttendanceRepository : BaseRepository<AttendanceTb>, IAttendanceRepository
    {
        private readonly StudentdbContext _context;

        public AttendanceRepository(StudentdbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
