using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitofwork;

        public StudentService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        // ---------------------- Get All -----------------------
        public async Task<IEnumerable<Student_VM>> GetAllAsync()
        {
            var students = await _unitofwork.StudentRepository.GetAllAsync();

            return students.Select(s => new Student_VM
            {
                StudentPkid = s.StudentPkid,
                StudentId = s.StudentId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                Email = s.Email,

            }).ToList();
        }

        // ---------------------- Get By ID -----------------------
        public async Task<Student_VM> GetByIdAsync(int id)
        {
            var student = await _unitofwork.StudentRepository.GetStudentWithClassAsync(id);
            if (student == null) return null;

            return new Student_VM
            {
                StudentPkid = student.StudentPkid,
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,

            };
        }

        // ------------------------ Create -------------------------
        public async Task CreateAsync(Student_VM vm)
        {
            var entity = new StudentTb
            {
                FullName = vm.FullName,
                StudentId = vm.StudentId,
                DateOfBirth = vm.DateOfBirth,
                Email = vm.Email,

            };

            await _unitofwork.StudentRepository.CreateAsync(entity);
            _unitofwork.Commit();
        }

        // ------------------------ Update -------------------------
        public async Task UpdateAsync(Student_VM vm)
        {
            var entity = await _unitofwork.StudentRepository.GetSingleAsync(x => x.StudentPkid == vm.StudentPkid);
            if (entity == null) return;

            entity.StudentId = vm.StudentId;
            entity.FullName = vm.FullName;
            entity.DateOfBirth = vm.DateOfBirth;
            entity.Email = vm.Email;


            _unitofwork.StudentRepository.Update(entity);
            _unitofwork.Commit();
        }

        // ------------------------ Delete -------------------------
        public async Task DeleteAsync(int id)
        {
            var entity = await _unitofwork.StudentRepository.GetSingleAsync(x => x.StudentPkid == id);
            if (entity == null) return;

            _unitofwork.StudentRepository.Delete(entity);
            _unitofwork.Commit();
        }
    }
}
