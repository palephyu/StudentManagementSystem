using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
