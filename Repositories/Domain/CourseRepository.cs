using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class CourseRepository : BaseRepository<CourseTb>, ICourseRepository
    {
        private readonly StudentdbContext context;

        public CourseRepository(StudentdbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
