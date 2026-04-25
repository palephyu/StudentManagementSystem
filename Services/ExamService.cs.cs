using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public class ExamService: IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Exam_VM>> GetAllAsync()
        {
            var data = await _unitOfWork.ExamRepository.GetAllAsync();

            return data.Select(x => new Exam_VM
            {
                ExamPkid = x.ExamPkid,
                CoursePkid = x.CoursePkid,
                ClassPkid = x.ClassPkid,
                ExamTitle = x.ExamTitle,
                ExamDate = x.ExamDate,
                MaxMarks = x.MaxMarks,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Location = x.Location
            });
        }

        public async Task<Exam_VM> GetByIdAsync(int id)
        {
            var x = await _unitOfWork.ExamRepository.GetByIdAsync(id);

            return new Exam_VM
            {
                ExamPkid = x.ExamPkid,
                CoursePkid = x.CoursePkid,
                ClassPkid = x.ClassPkid,
                ExamTitle = x.ExamTitle,
                ExamDate = x.ExamDate,
                MaxMarks = x.MaxMarks,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Location = x.Location
            };
        }

        public async Task CreateAsync(Exam_VM vm)
        {
            var entity = new ExamTb
            {
                CoursePkid = vm.CoursePkid,
                ClassPkid = vm.ClassPkid,
                ExamTitle = vm.ExamTitle,
                ExamDate = vm.ExamDate,
                MaxMarks = vm.MaxMarks,
                StartTime = vm.StartTime,
                EndTime = vm.EndTime,
                Location = vm.Location,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.ExamRepository.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task UpdateAsync(Exam_VM vm)
        {
            var entity = await _unitOfWork.ExamRepository.GetByIdAsync(vm.ExamPkid);

            entity.ExamTitle = vm.ExamTitle;
            entity.ExamDate = vm.ExamDate;
            entity.MaxMarks = vm.MaxMarks;
            entity.StartTime = vm.StartTime;
            entity.EndTime = vm.EndTime;
            entity.Location = vm.Location;
            entity.ModifiedDate = DateTime.Now;

            _unitOfWork.ExamRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.ExamRepository.GetByIdAsync(id);

            _unitOfWork.ExamRepository.Delete(entity);
            await _unitOfWork.Commit();
        }
    }
}