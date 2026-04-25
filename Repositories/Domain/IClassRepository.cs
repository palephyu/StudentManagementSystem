using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;
using StudentManagementSystem.ViewModels;

// Repositories/Domain/IClassRepository.cs


namespace StudentManagementSystem.Repositories.Domain
{
    public interface IClassRepository : IBaseRepository<ClassTb>
    {
        Task<IEnumerable<ClassTb>> GetAllActiveAsync();
        Task<IEnumerable<ClassTb>> SearchAsync(string? className, int? year, string? section, int? teacherId);
        Task<ClassTb?> GetClassByIdAsync(int classId);
        Task<bool> IsClassNameExistsAsync(string className, string section, int? excludeId = null);

        // Manual join methods (No FK)
        Task<int> GetStudentCountAsync(int classId);
        Task<int> GetCourseCountAsync(int classId);
        Task<IEnumerable<dynamic>> GetStudentsByClassAsync(int classId);
        Task<IEnumerable<dynamic>> GetCoursesByClassAsync(int classId);

        // Soft delete
        Task SoftDeleteAsync(int classId, int deletedBy);
        Task RestoreAsync(int classId);

        // Manual relationship management
        Task AddStudentToClassAsync(int classId, int studentId);
        Task RemoveStudentFromClassAsync(int classId, int studentId);
        Task AddCourseToClassAsync(int classId, int courseId, int? teacherId = null);
        Task RemoveCourseFromClassAsync(int classId, int courseId);
    }
}