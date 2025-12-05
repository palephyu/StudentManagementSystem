using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }
        // GET: /Teacher
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAll();   
            return View(list);
        }


        // GET: /Teacher/Create
        public  IActionResult Create() {

            var vm = new Teacher_VM
            {
                //HireDate = DateTime.UtcNow
            };
            return View(vm);
        }

        // POST: /Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher_VM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _service.Create(vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
