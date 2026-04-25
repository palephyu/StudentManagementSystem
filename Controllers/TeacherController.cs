using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        // GET: /Teacher/Index
        // ဆရာစာရင်းကို ပြမယ်
        public async Task<IActionResult> Index()
        {
            try
            {
                var teachers = await _teacherService.GetAllAsync();
                return View(teachers); // View ကို Data ပို့တယ်
            }
            catch (Exception ex)
            {
                TempData["Error"] = "ဆရာစာရင်း ယူရာတွင် အမှားရှိသည်";
                return View(new List<Teacher_VM>());
            }
        }

        // GET: /Teacher/Create
        // ဆရာအသစ် ဖန်တီးတဲ့ Form ကိုပြမယ်
        public IActionResult Create()
        {
            return View(new Teacher_VM());
        }

        // POST: /Teacher/Create
        // Form ကနေ Data လက်ခံပြီး သိမ်းမယ်
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher_VM vm)
        {
            // Model Validation စစ်တယ်
            if (!ModelState.IsValid)
            {
                return View(vm); // Error ရှိရင် Form ကိုပြန်ပြမယ်
            }

            try
            {
                // Service ကို ခေါ်ပြီး သိမ်းမယ်
                await _teacherService.CreateAsync(vm);

                TempData["Success"] = "ဆရာ အသစ် ဖန်တီးခြင်း အောင်မြင်ပါသည်";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        // GET: /Teacher/Edit/5
        // ဆရာအချက်အလက် ပြင်တဲ့ Form ကိုပြမယ်
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        // ပြင်ဆင်ထားတဲ့ Data ကို လက်ခံပြီး သိမ်းမယ်
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teacher_VM vm)
        {
            if (id != vm.TeacherPkid)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _teacherService.UpdateAsync(vm);
                TempData["Success"] = "ဆရာအချက်အလက် ပြင်ဆင်ခြင်း အောင်မြင်ပါသည်";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        // GET: /Teacher/Delete/5
        // ဖျက်တော့မယ် Confirm မေးမယ်
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: /Teacher/Delete/5
        // တကယ်ဖျက်မယ်
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _teacherService.DeleteAsync(id);
                TempData["Success"] = "ဆရာ ဖျက်ခြင်း အောင်မြင်ပါသည်";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        // ==================== DETAILS ACTION ====================
        // GET: /Teacher/Details/5
        // ဆရာတစ်ဦး၏ အသေးစိတ်အချက်အလက်များကို ပြသမည်
        public async Task<IActionResult> Details(int id)
        {
            // Parameter validation
            if (id <= 0)
            {
                _logger.LogWarning("Invalid teacher ID received: {Id}", id);
                TempData["Error"] = "မှားယွင်းသော ဆရာ ID ဖြစ်ပါသည်။";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Get teacher by ID from service
                var teacher = await _teacherService.GetByIdAsync(id);

                // Check if teacher exists
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID {Id} not found", id);
                    TempData["Error"] = $"ဆရာ ID {id} ကို မတွေ့ရှိပါ။";
                    return RedirectToAction(nameof(Index));
                }

                // Return view with teacher data
                return View(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading teacher details for ID: {Id}", id);
                TempData["Error"] = "ဆရာအချက်အလက်များ ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return RedirectToAction(nameof(Index));
            }
        }

        // ==================== DETAILS WITH RELATED DATA ====================
        // GET: /Teacher/DetailsWithRelated/5
        // ဆရာ၏ အသေးစိတ်နှင့် ဆက်စပ်သင်တန်းများကို ပြသမည် (Advanced)
        //public async Task<IActionResult> DetailsWithRelated(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest("Invalid teacher ID");
        //    }

        //    try
        //    {
        //        // You would need to create a new ViewModel that includes courses
        //        var teacher = await _teacherService.GetTeacherWithDetailsAsync(id);

        //        if (teacher == null)
        //        {
        //            return NotFound($"Teacher with ID {id} not found");
        //        }

        //        return View(teacher);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error loading teacher details with related data for ID: {Id}", id);
        //        TempData["Error"] = "An error occurred while loading teacher details.";
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

    }
}
