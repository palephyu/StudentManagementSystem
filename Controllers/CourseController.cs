using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
