using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
