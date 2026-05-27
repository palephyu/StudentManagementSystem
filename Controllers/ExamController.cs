using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Services;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ExamController(IExamService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Exam_VM vm)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(vm);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Exam_VM vm)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(vm);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }
        private async Task PopulateDropdowns(object? selectedCourseId = null, object? selectedClassId = null)
        {
            try
            {
                // Temporary: Get all (including soft deleted)
                var courses = await _unitOfWork.CourseRepository.GetAllAsync();  // instead of GetAllActiveAsync
                var classes = await _unitOfWork.ClassRepository.GetAllAsync();    // instead of GetAllActiveAsync

                ViewBag.CourseList = new SelectList(courses, "CoursePkid", "CourseName", selectedCourseId);
                ViewBag.ClassList = new SelectList(classes, "ClassPkid", "ClassName", selectedClassId);

                // Debug: Check if data exists
                System.Diagnostics.Debug.WriteLine($"Courses count: {courses.Count()}");
                System.Diagnostics.Debug.WriteLine($"Classes count: {classes.Count()}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                ViewBag.CourseList = new SelectList(new List<Course_VM>(), "CoursePkid", "CourseName");
                ViewBag.ClassList = new SelectList(new List<Class_VM>(), "ClassPkid", "ClassName");
            }
        }
    }
}
