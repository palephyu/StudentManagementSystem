// Services/ClassService.cs
using Microsoft.Extensions.Logging;
using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClassService> _logger;

        public ClassService(IUnitOfWork unitOfWork, ILogger<ClassService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Class_VM>> GetAllAsync()
        {
            try
            {
                var classes = await _unitOfWork.ClassRepository.GetAllAsync();

                if (classes == null)
                    return new List<Class_VM>();

                return await MapToViewModelList(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all classes");
                return new List<Class_VM>();
            }
        }
        public async Task<IEnumerable<Class_VM>> GetAllActiveAsync()
        {
            try
            {
                _logger.LogInformation("GetAllActiveAsync started");

                var classes = await _unitOfWork.ClassRepository.GetAllActiveAsync();

                _logger.LogInformation("Retrieved {Count} classes from repository", classes?.Count() ?? 0);

                if (classes == null || !classes.Any())
                {
                    _logger.LogWarning("No classes found");
                    return new List<Class_VM>();
                }

                var result = await MapToViewModelList(classes);
                _logger.LogInformation("Mapped {Count} classes to ViewModel", result?.Count() ?? 0);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active classes");
                return new List<Class_VM>();
            }
        }
        public async Task<Class_VM?> GetByIdAsync(int id)
        {
            try
            {
                var classEntity = await _unitOfWork.ClassRepository.GetClassByIdAsync(id);

                if (classEntity == null)
                    return null;

                return await MapToViewModel(classEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class by id: {Id}", id);
                throw;
            }
        }

        public async Task<ClassDetail_VM?> GetClassDetailsAsync(int id)
        {
            try
            {
                var classEntity = await _unitOfWork.ClassRepository.GetClassByIdAsync(id);

                if (classEntity == null)
                    return null;

                var classVM = await MapToViewModel(classEntity);

                // Get students using manual join
                var studentsData = await _unitOfWork.ClassRepository.GetStudentsByClassAsync(id);
                var students = new List<StudentInfo_VM>();

                foreach (var s in studentsData)
                {
                    students.Add(new StudentInfo_VM
                    {
                        StudentPkid = s.StudentPkid,
                        StudentId = s.StudentId,
                        FullName = s.FullName,
                        Email = s.Email,
                        Phone = s.Phone
                    });
                }

                // Get courses using manual join
                var coursesData = await _unitOfWork.ClassRepository.GetCoursesByClassAsync(id);
                var courses = new List<CourseInfo_VM>();

                foreach (var c in coursesData)
                {
                    courses.Add(new CourseInfo_VM
                    {
                        CoursePkid = c.CoursePkid,
                        CourseCode = c.CourseCode,
                        CourseName = c.CourseName,
                        Credits = c.Credits
                    });
                }

                var detailVM = new ClassDetail_VM
                {
                    ClassPkid = classVM.ClassPkid,
                    ClassName = classVM.ClassName,
                    Year = classVM.Year,
                    Section = classVM.Section,
                    TeacherPkid = classVM.TeacherPkid,
                    TeacherName = classVM.TeacherName,
                    IsDeleted = classVM.IsDeleted,
                    CreatedBy = classVM.CreatedBy,
                    CreatedDate = classVM.CreatedDate,
                    ModifiedBy = classVM.ModifiedBy,
                    ModifiedDate = classVM.ModifiedDate,
                    TotalStudents = students.Count,
                    TotalCourses = courses.Count,
                    Students = students,
                    Courses = courses
                };

                return detailVM;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class details for id: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Class_VM>> SearchAsync(ClassSearch_VM searchVM)
        {
            try
            {
                var classes = await _unitOfWork.ClassRepository.SearchAsync(
                    searchVM.ClassName,
                    searchVM.Year,
                    searchVM.Section,
                    searchVM.TeacherPkid
                );

                if (classes == null)
                    return new List<Class_VM>();

                return await MapToViewModelList(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching classes");
                return new List<Class_VM>();
            }
        }
        public async Task CreateAsync(Class_VM vm)
        {
            try
            {
                // Check if class name with same section exists
                if (await IsClassNameExistsAsync(vm.ClassName, vm.Section))
                {
                    throw new Exception($"အတန်း {vm.ClassName} - {vm.Section} ရှိပြီးသားပါ။");
                }

                // Validate year
                if (vm.Year.HasValue && (vm.Year < 1900 || vm.Year > DateTime.Now.Year + 10))
                {
                    throw new Exception("သင်နှစ်သည် 1900 မှ လက်ရှိနှစ် +10 အတွင်းဖြစ်ရပါမည်။");
                }

                var currentUserId = GetCurrentUserId();

                var classEntity = new ClassTb
                {
                    ClassName = vm.ClassName,
                    Year = vm.Year ?? DateTime.Now.Year,
                    Section = vm.Section?.ToUpper(),
                    TeacherPkid = vm.TeacherPkid,
                    IsDeleted = false,
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = currentUserId,
                    ModifiedDate = DateTime.Now
                };

                await _unitOfWork.ClassRepository.CreateAsync(classEntity);
                await _unitOfWork.Commit();

                _logger.LogInformation("Class created: {ClassName} - {Section}", vm.ClassName, vm.Section);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating class");
                 _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateAsync(Class_VM vm)
        {
            try
            {
                // Check if class name with same section exists (excluding current)
                if (await IsClassNameExistsAsync(vm.ClassName, vm.Section, vm.ClassPkid))
                {
                    throw new Exception($"အတန်း {vm.ClassName} - {vm.Section} ရှိပြီးသားပါ။");
                }

                var classEntity = await _unitOfWork.ClassRepository.GetClassByIdAsync(vm.ClassPkid);

                if (classEntity == null)
                    throw new Exception($"Class with id {vm.ClassPkid} not found");

                // Update properties
                classEntity.ClassName = vm.ClassName;
                classEntity.Year = vm.Year;
                classEntity.Section = vm.Section?.ToUpper();
                classEntity.TeacherPkid = vm.TeacherPkid;
                classEntity.ModifiedBy = GetCurrentUserId();
                classEntity.ModifiedDate = DateTime.Now;

                _unitOfWork.ClassRepository.Update(classEntity);
                await _unitOfWork.Commit();

                _logger.LogInformation("Class updated: {ClassName} - {Section}", vm.ClassName, vm.Section);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating class with id: {Id}", vm.ClassPkid);
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteAsync(int id, int deletedBy)
        {
            try
            {
                var classEntity = await _unitOfWork.ClassRepository.GetClassByIdAsync(id);

                if (classEntity == null)
                    throw new Exception($"Class with id {id} not found");

                // Check if class has students
                var studentCount = await _unitOfWork.ClassRepository.GetStudentCountAsync(id);
                if (studentCount > 0)
                {
                    throw new Exception($"ဤအတန်းတွင် ကျောင်းသား {studentCount} ဦးရှိနေသောကြောင့် ဖျက်၍မရပါ။");
                }

                // Soft delete
                await _unitOfWork.ClassRepository.SoftDeleteAsync(id, deletedBy);
                await _unitOfWork.Commit();

                _logger.LogInformation("Class soft deleted: {ClassName}", classEntity.ClassName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting class with id: {Id}", id);
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task RestoreAsync(int id)
        {
            try
            {
                await _unitOfWork.ClassRepository.RestoreAsync(id);
                await _unitOfWork.Commit();

                _logger.LogInformation("Class restored: {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error restoring class with id: {Id}", id);
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task<bool> IsClassNameExistsAsync(string className, string section, int? excludeId = null)
        {
            return await _unitOfWork.ClassRepository.IsClassNameExistsAsync(className, section, excludeId);
        }

        public async Task<IEnumerable<Class_VM>> GetDeletedClassesAsync()
        {
            try
            {
                var classes = await _unitOfWork.ClassRepository.FindAsync(c => c.IsDeleted == true);

                if (classes == null)
                    return new List<Class_VM>();

                return await MapToViewModelList(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting deleted classes");
                return new List<Class_VM>();
            }
        }
        public async Task AddStudentToClassAsync(int classId, int studentId)
        {
            try
            {
                await _unitOfWork.ClassRepository.AddStudentToClassAsync(classId, studentId);
                await _unitOfWork.Commit();
                _logger.LogInformation("Student {StudentId} added to class {ClassId}", studentId, classId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student to class");
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task RemoveStudentFromClassAsync(int classId, int studentId)
        {
            try
            {
                await _unitOfWork.ClassRepository.RemoveStudentFromClassAsync(classId, studentId);
                await _unitOfWork.Commit();
                _logger.LogInformation("Student {StudentId} removed from class {ClassId}", studentId, classId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing student from class");
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task AddCourseToClassAsync(int classId, int courseId, int? teacherId = null)
        {
            try
            {
                await _unitOfWork.ClassRepository.AddCourseToClassAsync(classId, courseId, teacherId);
                await _unitOfWork.Commit();
                _logger.LogInformation("Course {CourseId} added to class {ClassId}", courseId, classId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding course to class");
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task RemoveCourseFromClassAsync(int classId, int courseId)
        {
            try
            {
                await _unitOfWork.ClassRepository.RemoveCourseFromClassAsync(classId, courseId);
                await _unitOfWork.Commit();
                _logger.LogInformation("Course {CourseId} removed from class {ClassId}", courseId, classId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing course from class");
                _unitOfWork.RollBack();
                throw;
            }
        }

        // Private helper methods
        private async Task<Class_VM> MapToViewModel(ClassTb entity)
        {
            try
            {
                _logger.LogInformation("Mapping class: ID={Id}, Name={Name}", entity.Classpkid, entity.ClassName);

                string? teacherName = null;

                if (entity.TeacherPkid.HasValue)
                {
                    try
                    {
                        var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(entity.TeacherPkid.Value);
                        teacherName = teacher?.FullName;
                        _logger.LogInformation("Found teacher: {TeacherName}", teacherName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error loading teacher for ID {TeacherId}", entity.TeacherPkid);
                    }
                }

                int studentCount = 0;
                int courseCount = 0;

                try
                {
                    studentCount = await _unitOfWork.ClassRepository.GetStudentCountAsync(entity.Classpkid);
                    courseCount = await _unitOfWork.ClassRepository.GetCourseCountAsync(entity.Classpkid);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting counts for class {ClassId}", entity.Classpkid);
                }

                return new Class_VM
                {
                    ClassPkid = entity.Classpkid,
                    ClassName = entity.ClassName,
                    Year = entity.Year,
                    Section = entity.Section,
                    TeacherPkid = entity.TeacherPkid,
                    TeacherName = teacherName,
                    IsDeleted = entity.IsDeleted,
                    CreatedBy = GetUserName(entity.CreatedBy),
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = GetUserName(entity.ModifiedBy),
                    ModifiedDate = entity.ModifiedDate,
                    TotalStudents = studentCount,
                    TotalCourses = courseCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error mapping class entity to ViewModel for ID {ClassId}", entity?.Classpkid);
                throw;
            }
        }
        private async Task<IEnumerable<Class_VM>> MapToViewModelList(IEnumerable<ClassTb> entities)
        {
            if (entities == null)
                return new List<Class_VM>();

            var result = new List<Class_VM>();

            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    result.Add(await MapToViewModel(entity));  // This might be failing
                }
            }

            return result;
        }
        private int GetCurrentUserId()
        {
            // Get from session or HttpContext
            return 1; // Placeholder
        }

        private string? GetUserName(int? userId)
        {
            if (!userId.HasValue) return null;
            // Get from database or session
            return userId.ToString(); // Placeholder
        }
    }
}