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
                Address = s.Address,
                Phone = s.Phone,
                Gender = s.Gender,
                ImagePath = s.ImagePath,
              // EnrollmentDate = DateOnly.FromDateTime(DateTime.Now), // Fixed: use 'DateOnly' (capital D)
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
                Address = student.Address,
                Phone = student.Phone,
                Gender = student.Gender,
                ImagePath = student.ImagePath,
                EnrollmentDate = student.EnrollmentDate,

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
                Gender = vm.Gender,
                Address = vm.Address,
                Phone = vm.Phone,
                ImagePath = vm.ImagePath,
                Email = vm.Email,
                EnrollmentDate = DateOnly.FromDateTime(DateTime.Now), // Fixed: use 'DateOnly' (capital D)
            };

            await _unitofwork.StudentRepository.CreateAsync(entity);
            await _unitofwork.Commit();
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
            entity.Address = vm.Address;
            entity.Phone = vm.Phone;
            entity.ImagePath = vm.ImagePath;
            entity.Gender = vm.Gender;
            entity.EnrollmentDate = DateOnly.FromDateTime(DateTime.Now); // Fixed: use 'DateOnly' (capital D)

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
