using StudentManagementSystem.ViewModels;
// Business Logic အတွက် သတ်မှတ်ချက်

namespace StudentManagementSystem.Services

{
    public interface ITeacherService
    {
        // CRUD Operations (ViewModel နဲ့ အလုပ်လုပ်တယ်)
        Task<IEnumerable<Teacher_VM>> GetAllAsync();
        Task<Teacher_VM> GetByIdAsync(int id);
        Task CreateAsync(Teacher_VM vm);
        Task UpdateAsync(Teacher_VM vm);
        Task DeleteAsync(int id);

        // Business Logic သီးသန့်
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
