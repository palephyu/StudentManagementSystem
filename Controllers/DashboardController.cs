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
            // Role protection
            //if (HttpContext.Session.GetString("UserRole") != "Teacher")
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            return View();
        }

        public IActionResult StudentDashboard()
        {
            // Role protection
            //if (HttpContext.Session.GetString("UserRole") != "Student")
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            return View();
        }
    }

}
