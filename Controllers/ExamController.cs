using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamService _service;

        public ExamController(IExamService service)
        {
            _service = service;
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
    }
}
