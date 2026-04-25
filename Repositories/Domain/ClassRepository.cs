// Repositories/Domain/ClassRepository.cs
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class ClassRepository : BaseRepository<ClassTb>, IClassRepository
    {
        private readonly StudentdbContext _context;
        private readonly DbSet<ClassTb> _dbSet;
        public ClassRepository(StudentdbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<ClassTb>();  
        }

        public async Task<IEnumerable<ClassTb>> GetAllActiveAsync()
        {
            // Manual query without navigation properties
            var query = @"SELECT * FROM Classes WHERE IsDeleted != 1 ORDER BY ClassName, Section";

            // Or using LINQ
            return await _dbSet
                .Where(c => c.IsDeleted != true)
                .OrderBy(c => c.ClassName)
                .ThenBy(c => c.Section)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassTb>> SearchAsync(string? className, int? year, string? section, int? teacherId)
        {
            var query = _dbSet.Where(c => c.IsDeleted != true);

            if (!string.IsNullOrEmpty(className))
                query = query.Where(c => c.ClassName != null && c.ClassName.Contains(className));

            if (year.HasValue)
                query = query.Where(c => c.Year == year);

            if (!string.IsNullOrEmpty(section))
                query = query.Where(c => c.Section != null && c.Section.Contains(section));

            if (teacherId.HasValue)
                query = query.Where(c => c.TeacherPkid == teacherId);

            return await query
                .OrderBy(c => c.ClassName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ClassTb?> GetClassByIdAsync(int classId)
        {
            return await _dbSet
                .Where(c => c.Classpkid == classId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsClassNameExistsAsync(string className, string section, int? excludeId = null)
        {
            var query = _dbSet.Where(c => c.ClassName == className && c.Section == section && c.IsDeleted != true);

            if (excludeId.HasValue)
                query = query.Where(c => c.Classpkid != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<int> GetStudentCountAsync(int classId)
        {
            // Manual SQL query without foreign key
            var query = @"SELECT COUNT(*) FROM ClassStudents 
                         WHERE ClassPkid = {0} AND IsActive = 1";

            var count = await _context.Database
                .SqlQueryRaw<int>(query, classId)
                .FirstOrDefaultAsync();

            return count;

            // Or using LINQ
            // return await _context.Set<ClassStudent>()
            //     .Where(cs => cs.ClassPkid == classId && cs.IsActive)
            //     .CountAsync();
        }

        public async Task<int> GetCourseCountAsync(int classId)
        {
            // Manual SQL query without foreign key
            var query = @"SELECT COUNT(*) FROM ClassCourses 
                         WHERE ClassPkid = {0} AND IsActive = 1";

            var count = await _context.Database
                .SqlQueryRaw<int>(query, classId)
                .FirstOrDefaultAsync();

            return count;
        }

        public async Task<IEnumerable<dynamic>> GetStudentsByClassAsync(int classId)
        {
            // Manual JOIN without navigation properties
            var query = @"
                SELECT s.StudentPkid, s.StudentId, s.FullName, s.Email, s.Phone
                FROM Students s
                INNER JOIN ClassStudents cs ON s.StudentPkid = cs.StudentPkid
                WHERE cs.ClassPkid = {0} AND cs.IsActive = 1 AND s.IsActive = 1
                ORDER BY s.StudentId";

            var students = await _context.Database
                .SqlQueryRaw<StudentInfo>(query, classId)
                .ToListAsync();

            return students;
        }

        public async Task<IEnumerable<dynamic>> GetCoursesByClassAsync(int classId)
        {
            // Manual JOIN without navigation properties
            var query = @"
                SELECT c.CoursePkid, c.CourseCode, c.CourseName, c.Credits
                FROM Courses c
                INNER JOIN ClassCourses cc ON c.CoursePkid = cc.CoursePkid
                WHERE cc.ClassPkid = {0} AND cc.IsActive = 1
                ORDER BY c.CourseCode";

            var courses = await _context.Database
                .SqlQueryRaw<CourseInfo>(query, classId)
                .ToListAsync();

            return courses;
        }

        public async Task SoftDeleteAsync(int classId, int deletedBy)
        {
            // Manual update without navigation
            var query = @"
                UPDATE Classes 
                SET IsDeleted = 1, ModifiedBy = {1}, ModifiedDate = GETDATE()
                WHERE ClassPkid = {0}";

            await _context.Database.ExecuteSqlRawAsync(query, classId, deletedBy);
        }

        public async Task RestoreAsync(int classId)
        {
            // Manual update without navigation
            var query = @"
                UPDATE Classes 
                SET IsDeleted = 0, ModifiedDate = GETDATE()
                WHERE ClassPkid = {0}";

            await _context.Database.ExecuteSqlRawAsync(query, classId);
        }

        public async Task AddStudentToClassAsync(int classId, int studentId)
        {
            // Check if already exists
            var checkQuery = @"
                SELECT COUNT(*) FROM ClassStudents 
                WHERE ClassPkid = {0} AND StudentPkid = {1}";

            var exists = await _context.Database
                .SqlQueryRaw<int>(checkQuery, classId, studentId)
                .FirstOrDefaultAsync() > 0;

            if (!exists)
            {
                var insertQuery = @"
                    INSERT INTO ClassStudents (ClassPkid, StudentPkid, EnrollmentDate, IsActive)
                    VALUES ({0}, {1}, GETDATE(), 1)";

                await _context.Database.ExecuteSqlRawAsync(insertQuery, classId, studentId);
            }
        }

        public async Task RemoveStudentFromClassAsync(int classId, int studentId)
        {
            var deleteQuery = @"
                DELETE FROM ClassStudents 
                WHERE ClassPkid = {0} AND StudentPkid = {1}";

            await _context.Database.ExecuteSqlRawAsync(deleteQuery, classId, studentId);
        }

        public async Task AddCourseToClassAsync(int classId, int courseId, int? teacherId = null)
        {
            // Check if already exists
            var checkQuery = @"
                SELECT COUNT(*) FROM ClassCourses 
                WHERE ClassPkid = {0} AND CoursePkid = {1}";

            var exists = await _context.Database
                .SqlQueryRaw<int>(checkQuery, classId, courseId)
                .FirstOrDefaultAsync() > 0;

            if (!exists)
            {
                var insertQuery = @"
                    INSERT INTO ClassCourses (ClassPkid, CoursePkid, TeacherPkid, AssignedDate, IsActive)
                    VALUES ({0}, {1}, {2}, GETDATE(), 1)";

                await _context.Database.ExecuteSqlRawAsync(insertQuery, classId, courseId, teacherId ?? (object)DBNull.Value);
            }
        }

        public async Task RemoveCourseFromClassAsync(int classId, int courseId)
        {
            var deleteQuery = @"
                DELETE FROM ClassCourses 
                WHERE ClassPkid = {0} AND CoursePkid = {1}";

            await _context.Database.ExecuteSqlRawAsync(deleteQuery, classId, courseId);
        }
    }

    // Helper classes for raw SQL mapping
    public class StudentInfo
    {
        public int StudentPkid { get; set; }
        public string? StudentId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class CourseInfo
    {
        public int CoursePkid { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public int? Credits { get; set; }
    }
}