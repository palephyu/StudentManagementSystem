using StudentManagementSystem.DAO;

namespace StudentManagementSystem.Repositories.Domain
{
    public class EnrollmentRepository 
    {
        private StudentdbContext context;

        public EnrollmentRepository(StudentdbContext context)
        {
            this.context = context;
        }
    }
}
