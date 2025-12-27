using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
