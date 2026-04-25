// Controllers/ClassController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly ILogger<ClassController> _logger;

        public ClassController(
            IClassService classService,
            ITeacherService teacherService,
            IStudentService studentService,
            ICourseService courseService,
            ILogger<ClassController> logger)
        {
            _classService = classService;
            _teacherService = teacherService;
            _studentService = studentService;
            _courseService = courseService;
            _logger = logger;
        }

        // GET: Class/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var classes = await _classService.GetAllActiveAsync();
                return View(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading class index");
                TempData["Error"] = "အတန်းများစာရင်း ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return View(new List<Class_VM>());
            }
        }

        // GET: Class/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var classDetail = await _classService.GetClassDetailsAsync(id);

                if (classDetail == null)
                {
                    TempData["Error"] = "အတန်း မတွေ့ရှိပါ။";
                    return RedirectToAction(nameof(Index));
                }

                return View(classDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading class details for id: {Id}", id);
                TempData["Error"] = "အတန်းအချက်အလက်များ ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Class/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                // Make sure to populate the dropdown
                await PopulateTeacherDropdown();

                return View(new Class_VM
                {
                    Year = DateTime.Now.Year
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create page");
                TempData["Error"] = "အတန်းအသစ်ဖန်တီးရန် စာမျက်နှာ ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return View(new Class_VM());
            }
        }


        // POST: Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class_VM vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateTeacherDropdown(vm.TeacherPkid);
                return View(vm);
            }

            try
            {
                await _classService.CreateAsync(vm);
                TempData["Success"] = $"အတန်း {vm.ClassName} - {vm.Section} ကို အောင်မြင်စွာ ဖန်တီးခဲ့သည်။";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating class");
                ModelState.AddModelError("", ex.Message);
                await PopulateTeacherDropdown(vm.TeacherPkid);
                return View(vm);
            }
        }

        // GET: Class/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var classEntity = await _classService.GetByIdAsync(id);

                if (classEntity == null)
                {
                    TempData["Error"] = "အတန်း မတွေ့ရှိပါ။";
                    return RedirectToAction(nameof(Index));
                }

                await PopulateTeacherDropdown(classEntity.TeacherPkid);
                return View(classEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading edit page for class id: {Id}", id);
                TempData["Error"] = "အတန်းအချက်အလက်များ ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Class_VM vm)
        {
            if (id != vm.ClassPkid)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateTeacherDropdown(vm.TeacherPkid);
                return View(vm);
            }

            try
            {
                await _classService.UpdateAsync(vm);
                TempData["Success"] = $"အတန်း {vm.ClassName} - {vm.Section} ကို အောင်မြင်စွာ ပြင်ဆင်ခဲ့သည်။";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating class with id: {Id}", id);
                ModelState.AddModelError("", ex.Message);
                await PopulateTeacherDropdown(vm.TeacherPkid);
                return View(vm);
            }
        }

        // GET: Class/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var classEntity = await _classService.GetByIdAsync(id);

                if (classEntity == null)
                {
                    TempData["Error"] = "အတန်း မတွေ့ရှိပါ။";
                    return RedirectToAction(nameof(Index));
                }

                return View(classEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading delete page for class id: {Id}", id);
                TempData["Error"] = "အတန်းအချက်အလက်များ ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var classEntity = await _classService.GetByIdAsync(id);
                string className = classEntity?.DisplayName ?? "Unknown";

                await _classService.DeleteAsync(id, GetCurrentUserId());
                TempData["Success"] = $"အတန်း {className} ကို အောင်မြင်စွာ ဖျက်ခဲ့သည်။";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting class with id: {Id}", id);
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Class/Search
        public async Task<IActionResult> Search(ClassSearch_VM searchVM)
        {
            try
            {
                var classes = await _classService.SearchAsync(searchVM);
                ViewBag.SearchTerm = searchVM.ClassName;
                return View("Index", classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching classes");
                TempData["Error"] = "ရှာဖွေမှုတွင် အမှားအယွင်းရှိပါသည်။";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Class/Deleted
        public async Task<IActionResult> Deleted()
        {
            try
            {
                var deletedClasses = await _classService.GetDeletedClassesAsync();
                return View(deletedClasses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading deleted classes");
                TempData["Error"] = "ဖျက်ထားသော အတန်းများ ယူဆောင်ရာတွင် အမှားအယွင်းရှိပါသည်။";
                return View(new List<Class_VM>());
            }
        }

        // POST: Class/Restore/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _classService.RestoreAsync(id);
                TempData["Success"] = "အတန်းကို ပြန်လည် အသက်သွင်းခဲ့သည်။";
                return RedirectToAction(nameof(Deleted));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error restoring class with id: {Id}", id);
                TempData["Error"] = "အတန်း ပြန်လည် အသက်သွင်းရာတွင် အမှားအယွင်းရှိပါသည်။";
                return RedirectToAction(nameof(Deleted));
            }
        }

        // AJAX Methods for managing students
        [HttpPost]
        public async Task<IActionResult> AddStudent(int classId, int studentId)
        {
            try
            {
                await _classService.AddStudentToClassAsync(classId, studentId);
                return Json(new { success = true, message = "ကျောင်းသား ထည့်သွင်းခြင်း အောင်မြင်ပါသည်။" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStudent(int classId, int studentId)
        {
            try
            {
                await _classService.RemoveStudentFromClassAsync(classId, studentId);
                return Json(new { success = true, message = "ကျောင်းသား ဖယ်ရှားခြင်း အောင်မြင်ပါသည်။" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Helper method with null handling
        private async Task PopulateTeacherDropdown(object? selectedValue = null)
        {
            try
            {
                var teachers = await _teacherService.GetAllAsync() ?? new List<Teacher_VM>();

                ViewBag.TeacherList = new SelectList(
                    teachers,
                    "TeacherPkid",
                    "FullName",
                    selectedValue
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating teacher dropdown");

                ViewBag.TeacherList = new SelectList(
                    new List<Teacher_VM>(),
                    "TeacherPkid",
                    "FullName"
                );
            }
        }
        private int GetCurrentUserId()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            return userId ?? 1;
        }

        // GET: Class/GetRecentClasses (for dashboard)
        [HttpGet]
        public async Task<IActionResult> GetRecentClasses()
        {
            try
            {
                var classes = await _classService.GetAllActiveAsync();
                var recentClasses = classes
                    .OrderByDescending(c => c.CreatedDate)
                    .Take(3)
                    .Select(c => new {
                        c.ClassPkid,
                        c.ClassName,
                        c.Section,
                        c.Year,
                        DisplayName = $"{c.ClassName} - {c.Section}"
                    });

                return Json(recentClasses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent classes");
                return Json(new List<object>());
            }
        }

        // GET: Class/GetTeacherInfo (for modal display)
        [HttpGet]
        public async Task<IActionResult> GetTeacherInfo(int id)
        {
            try
            {
                var teacher = await _teacherService.GetByIdAsync(id);

                if (teacher == null)
                    return Json(null);

                return Json(new
                {
                    teacher.FullName,
                    teacher.Email,
                    teacher.Phone,
                    teacher.Specialization
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teacher info for id: {Id}", id);
                return Json(null);
            }
        }
    }
}