using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Repositories.Domain;

namespace StudentManagementSystem.UnitOfWork
{
    public interface IUnitOfWork
    {      
        IStudentRepository StudentRepository { get; }
        IClassRepository ClassRepository { get; }
        IUserRepository UserRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ICourseRepository CourseRepository { get; }
        IExamRepository ExamRepository { get; }
        IExamResultRepository ExamResultRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        //   IEnrollmentRepository EnrollmentRepository { get; }

        // Transaction Methods
        Task Commit();   // အပြောင်းအလဲတွေကို Database မှာ သိမ်းမယ်     
        void RollBack(); // အပြောင်းအလဲတွေကို ပယ်ဖျက်မယ်
        Task BeginTransactionAsync();   // Transaction စမယ်
    }
}
