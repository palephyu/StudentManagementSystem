using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Services;
using StudentManagementSystem.ViewModels;
using System;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User_VM vm)
        {
            var user = _userService.Login(vm.Username, vm.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }

            // 🔀 Role-based Redirect
            if (user.Role == "Admin")
                return RedirectToAction("Index", "Home");

            if (user.Role == "Teacher")
                return RedirectToAction("TeacherDashboard", "Dashboard");

            if (user.Role == "Student")
                return RedirectToAction("StudentDashboard", "Dashboard");

            return View();
        }
    }

}
