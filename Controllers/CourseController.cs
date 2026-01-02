using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;
namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        public readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _service.GetAll(); // ✅ await
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Course_VM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _service.Create(vm);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id) 
        {
            var course = _service.GetById(id);
            return View(course);
        }
        public async Task<IActionResult> Update(int id) 
        {
            var course = await _service.GetById(id);
            return View(course);
        }
        public IActionResult Delete()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var course =await _service.GetById(id);
            return View(course);
        }
    }
}
