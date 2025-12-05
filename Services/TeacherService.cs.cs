using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _uow;

        public TeacherService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task Create(Teacher_VM vm)
        {

            var teachers = new TeacherTb
            {
                FullName = vm.FullName,
                Email = vm.Email,
                Phone = vm.Phone,
                Specialization = vm.Specialization,
                HireDate = vm.HireDate
            };

            await _uow.TeacherRepository.CreateAsync(teachers);
            await _uow.Commit();
        }

        public async Task<IEnumerable<Teacher_VM>> GetAll()
        {
            
            var teachers = await _uow.TeacherRepository.GetAllAsync();
            var list = teachers.Select(e => new Teacher_VM
            {
                TeacherPkid = e.TeacherPkid,
                FullName = e.FullName,
                Email = e.Email,
                HireDate = DateOnly.MinValue,
                Specialization = e.Specialization,
            });
            return list;    
        }
    }
}
