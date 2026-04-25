using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public interface IClassService
    {
        Task<IEnumerable<Class_VM>> GetAllAsync();
        Task<IEnumerable<Class_VM>> GetAllActiveAsync();
        Task<Class_VM?> GetByIdAsync(int id);
        Task<ClassDetail_VM?> GetClassDetailsAsync(int id);
        Task<IEnumerable<Class_VM>> SearchAsync(ClassSearch_VM searchVM);
        Task CreateAsync(Class_VM vm);
        Task UpdateAsync(Class_VM vm);
        Task DeleteAsync(int id, int deletedBy);
        Task RestoreAsync(int id);
        Task<bool> IsClassNameExistsAsync(string className, string section, int? excludeId = null);
        Task<IEnumerable<Class_VM>> GetDeletedClassesAsync();

        // Manual relationship management
        Task AddStudentToClassAsync(int classId, int studentId);
        Task RemoveStudentFromClassAsync(int classId, int studentId);
        Task AddCourseToClassAsync(int classId, int courseId, int? teacherId = null);
        Task RemoveCourseFromClassAsync(int classId, int courseId);
    }
}
