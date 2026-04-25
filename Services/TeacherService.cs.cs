using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(IUnitOfWork uow, ILogger<TeacherService> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        // ဆရာအားလုံးကို ယူမယ်
        // Services/TeacherService.cs
        public async Task<IEnumerable<Teacher_VM>> GetAllAsync()
        {
            try
            {
                var teachers = await _uow.TeacherRepository.GetAllAsync();

                // Always return empty list instead of null
                if (teachers == null || !teachers.Any())
                {
                    return new List<Teacher_VM>();
                }

                var teacherVMs = teachers.Select(t => new Teacher_VM
                {
                    TeacherPkid = t.TeacherPkid,
                    FullName = t.FullName,
                    Email = t.Email,
                    Phone = t.Phone,
                    Specialization = t.Specialization,
                    //HireDate = t.HireDate
                });

                return teacherVMs ?? new List<Teacher_VM>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all teachers");
                return new List<Teacher_VM>(); // Return empty list on error
            }
        }

        // ဆရာအသစ် ဖန်တီးမယ်
        public async Task CreateAsync(Teacher_VM vm)
        {
            // Business Logic - Validation
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            // Email ထပ်နေလား စစ်တယ်
            var existingTeacher = await _uow.TeacherRepository
                .FindAsync(t => t.Email == vm.Email);

            if (existingTeacher.Any())
                throw new Exception("ဒီအီးမေးလ် ရှိပြီးသားပါ");

            try
            {
                // ViewModel → Entity ပြောင်းတယ်
                var teacher = new TeacherTb
                {
                    FullName = vm.FullName,
                    Email = vm.Email,
                    Phone = vm.Phone,
                    HireDate = vm.HireDate == DateOnly.MinValue ?
                               DateOnly.FromDateTime(DateTime.Now) : vm.HireDate,
                    Specialization = vm.Specialization
                };

                // Repository ကို သိမ်းခိုင်းတယ်
                await _uow.TeacherRepository.CreateAsync(teacher);

                // Database မှာ သိမ်းတယ် (Commit)
                await _uow.Commit();

                _logger.LogInformation($"ဆရာ {teacher.FullName} ကို ဖန်တီးခဲ့သည်");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ဆရာ ဖန်တီးရာတွင် အမှားရှိသည်");
                 _uow.RollBack(); // Error ဖြစ်ရင် ပြန်ဖျက်မယ်
                throw;
            }
        }

        // ဆရာအချက်အလက် ပြင်မယ်
        public async Task UpdateAsync(Teacher_VM vm)
        {
            try
            {
                // ရှိလား စစ်တယ်
                var teacher = await _uow.TeacherRepository.GetByIdAsync(vm.TeacherPkid);

                if (teacher == null)
                    throw new Exception($"Teacher ID {vm.TeacherPkid} မတွေ့ပါ");

                // Entity ကို Update လုပ်တယ်
                teacher.FullName = vm.FullName;
                teacher.Email = vm.Email;
                teacher.Phone = vm.Phone;
                teacher.Specialization = vm.Specialization;
                teacher.HireDate = vm.HireDate;

                // Update လုပ်တယ်
                _uow.TeacherRepository.Update(teacher);

                // Database မှာ သိမ်းတယ်
                await _uow.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ဆရာ Update လုပ်ရာတွင် အမှားရှိသည်");
                _uow.RollBack();
                throw;
            }
        }

        // ဆရာကို ဖျက်မယ်
        public async Task DeleteAsync(int id)
        {
            try
            {
                var teacher = await _uow.TeacherRepository.GetByIdAsync(id);

                if (teacher == null)
                    throw new Exception($"Teacher ID {id} မတွေ့ပါ");

                // ဖျက်တယ်
                _uow.TeacherRepository.Delete(teacher);

                // Database မှာ သိမ်းတယ်
                await _uow.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ဆရာ ဖျက်ရာတွင် အမှားရှိသည်");
                _uow.RollBack();
                throw;
            }
        }

        // Email ထပ်နေလား စစ်တယ် (Business Rule)
        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var teachers = await _uow.TeacherRepository
                .FindAsync(t => t.Email == email);
            return !teachers.Any();
        }

        public async Task<Teacher_VM> GetByIdAsync(int id)
        {
            var teacher = await _uow.TeacherRepository.GetByIdAsync(id);
            if (teacher == null) return null;

            // Entity → ViewModel ပြောင်းတယ်
            var teacherVM = new Teacher_VM
            {
                TeacherPkid = teacher.TeacherPkid,
                FullName = teacher.FullName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                // HireDate = teacher.HireDate,
                Specialization = teacher.Specialization
            };

            return teacherVM;
        }
    }
}
