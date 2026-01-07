using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _service.GetByIdAsync(id);
            return View(student);
        }
        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public async Task<IActionResult> Create(Student_VM vm) 
        { 
            await _service.CreateAsync(vm); 
            return RedirectToAction("Index"); 
        }

        //ွGET:Edit
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _service.GetByIdAsync(id);
            return View(student);
        }
        //POST:Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Student_VM vm)
        {
            await _service.UpdateAsync(vm);
            return RedirectToAction("Index");
        }
        //GET:Delete
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _service.GetByIdAsync(id);
            return View(student);
        }
        //POST:Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }

}
