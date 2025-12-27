using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
