using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult TeacherDashboard()
        {
            return View();
        }

        public IActionResult StudentDashboard()
        {
            return View();
        }
    }

}
