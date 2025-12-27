using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers

{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // 🔹 User List       
        public async Task<IActionResult> Index()
        {
            //if (HttpContext.Session.GetString("Role") != "Admin")
            //    return RedirectToAction("Login", "Account");
            var list = await _service.GetAll();
            return View(list);
        }

        // 🔹 Open Create Form
        public IActionResult Create()
        {
            //if (HttpContext.Session.GetString("Role") != "Admin")
            //    return RedirectToAction("Login", "Account");

            var vm = new User_VM
            {
                //HireDate = DateTime.UtcNow
            };
            return View(vm);
        }

        // 🔹 Insert User (NO SQL)
        [HttpPost]
        public async Task<IActionResult> Create(User_VM vm)
        {
            //if (HttpContext.Session.GetString("Role") != "Admin")
            //    return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _service.Create(vm);

            return RedirectToAction("Index");
        }
        //public IActionResult Edit(int id)
        //{
        //    var user = _context.UserTbs.Find(id);
        //    return View(user);
        //}

        //[HttpPost]
        //public IActionResult Edit(UserTb user)
        //{
        //    _context.UserTbs.Update(user);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
