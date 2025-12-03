using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Repositories.Domain;

namespace StudentManagementSystem.UnitOfWork
{
    public interface IUnitOfWork
    {      
        IStudentRepository StudentRepository { get; }
        IClassRepository ClassRepository { get; }
        IUserRepository UserRepository { get; }
        //ITeacherRepository TeacherRepository { get; }
        //ICourseRepository CourseRepository { get; }
        //IExamRepository ExamRepository { get; }
        //IExamResultRepository ExamResultRepository { get; }
        //IAttendanceRepository AttendanceRepository { get; }
        //IEnrollmentRepository EnrollmentRepository { get; }
        Task Commit();       
        void RollBack();
    }
}
