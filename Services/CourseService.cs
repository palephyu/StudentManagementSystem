using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        public CourseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task Create(Course_VM vm)
        {
           var course = new CourseTb
            {
                CoursePkid = vm.CoursePkid,
                CourseName = vm.CourseName,
                Description = vm.Description,
                Credits = vm.Credits,
                CourseCode = vm.CourseCode
           };
           await _uow.CourseRepository.CreateAsync(course);
           await _uow.Commit();

        }

        public async Task Delete(int id)
        {
            var courses = await _uow.CourseRepository.FindAsync(e => e.CoursePkid == id);
            var course = courses.FirstOrDefault();
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            _uow.CourseRepository.Delete(course);
            await _uow.Commit();
        }

        public async Task<IEnumerable<Course_VM>> GetAll()
        {
            var courses = await _uow.CourseRepository.GetAllAsync();
            var list = courses.Select(e => new Course_VM
            {
                CoursePkid = e.CoursePkid,
                CourseName = e.CourseName,
                Description = e.Description,
                Credits = e.Credits,
                CourseCode = e.CourseCode
            });
            return list;
        }

        public async Task<Course_VM> GetById(int id)
        {
            //var course = await _uow.CourseRepository.FindAsync(id);
            //   if (course == null)
            //   {
            //      return null;
            // }
            //   var vm = new Course_VM
            // {
            //     CoursePkid = course.Result.CoursePkid,
            //     CourseName = course.Result.CourseName,
            //     Description = course.Result.Description,
            //     Credits = course.Result.Credits,
            //     CourseCode = course.Result.CourseCode
            // };
            // return vm;
            throw new NotImplementedException();
        }

        public Task Update(Course_VM vm)
        {
            var course = new CourseTb
            {
                CoursePkid = vm.CoursePkid,
                CourseName = vm.CourseName,
                Description = vm.Description,
                Credits = vm.Credits,
                CourseCode = vm.CourseCode
            };
            _uow.CourseRepository.Update(course);
            return _uow.Commit();

        }
    }
}
