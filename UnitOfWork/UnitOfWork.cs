using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Domain;

namespace StudentManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentdbContext _context;

        public IStudentRepository _studentRepository;
        public IClassRepository _classRepository;
        public ICourseRepository _courseRepository;
        public IEnrollmentRepository _enrollmentRepository;
        public ITeacherRepository _teacherRepository; 
        public IExamRepository _examResultRepository;
        public IExamRepository _examRepository;
        public IUserRepository _userRepository;
        public IAttendanceRepository _attendanceRepository;

     
        public UnitOfWork(StudentdbContext context)
        {
            _context = context;
          
        }
       
        public IStudentRepository StudentRepository
        {
            get
            {
                return _studentRepository = _studentRepository ?? new StudentRepository(_context);
            }
        }

        public IClassRepository ClassRepository
        {
            get
            {
                return _classRepository = _classRepository ?? new ClassRepository(_context );
            }
        }

        public IUserRepository UserRepository =>
            _userRepository = _userRepository ?? new UserRepository(_context);

        //public ITeacherRepository TeacherRepository => _teacherRepository = _teacherRepository ?? new TeacherRepository(_context);

        //public ICourseRepository CourseRepository =>
        //   _courseRepository ??= new CourseRepository(_context);

        //public IExamRepository ExamRepository =>
        //    _examRepository ??= new ExamRepository(_context);

        //public IExamResultRepository ExamResultRepository =>
        //    _examResultRepository ??= new ExamResultRepository(_context);

        //public IAttendanceRepository AttendanceRepository =>
        //    _attendanceRepository ??= new AttendanceRepository(_context);

        //public IEnrollmentRepository EnrollmentRepository =>
            //_enrollmentRepository ??= new EnrollmentRepository(_context);

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        public void RollBack()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }

 
}
