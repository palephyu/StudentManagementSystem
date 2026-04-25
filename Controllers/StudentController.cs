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
            try
            {
                var list = await _service.GetAllAsync();
                if (list == null)
                {
                    // Log လုပ်ပါ
                    return View(new List<Student_VM>());
                }
                return View(list);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in Index: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // User ကိုပြမယ်
                TempData["Error"] = "An error occurred: " + ex.Message;
                return View(new List<Student_VM>());
            }
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
