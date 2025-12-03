using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;
using StudentManagementSystem.ViewModels;
using System.Linq.Expressions;

namespace StudentManagementSystem.Repositories.Domain
{
    public class StudentRepository : BaseRepository<StudentTb>, IStudentRepository
    {
        private readonly StudentdbContext _context;
        public StudentRepository(StudentdbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Student_VM> GetSteudentByStudentId(string StudentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student_VM> GetStudents()
        {
            throw new NotImplementedException();
        }

        public async Task<Student_VM> GetStudentWithClassAsync(int id)
        {
            var getstudent = from s in _context.StudentTbs
                    join c in _context.ClassTbs on s.StudentPkid equals c.StudentPkid into cg
                    from c in cg.DefaultIfEmpty()
                    where s.StudentPkid == id
                    select new Student_VM
                    {
                        StudentPkid = s.StudentPkid,
                        StudentId = s.StudentId,
                        FullName = s.FullName,
                        DateOfBirth = s.DateOfBirth,
                        Email = s.Email,
                        Classpkid = id,
                        ClassName = c != null ? c.ClassName : null
                    };

            return await getstudent.FirstOrDefaultAsync();
        }


        public Task<bool> IsAlreadyExist(string studentId, string fullName)
        {
            return  _context.StudentTbs.AnyAsync(x => x.StudentId == studentId || x.FullName == fullName);
        }
        public async Task<string?> GetLastStudent()
        {
            return _context.StudentTbs.OrderByDescending(x => x.StudentPkid).Select(x => x.StudentId).FirstOrDefault();
        }



    }
}
