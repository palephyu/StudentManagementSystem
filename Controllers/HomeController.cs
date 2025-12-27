using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using System.Diagnostics;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WeatherApiService _weatherService;

        public HomeController(ILogger<HomeController> logger, WeatherApiService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Weather(string city = "Yangon")
        {
            if (string.IsNullOrWhiteSpace(city)) city = "Yangon";
            var model = await _weatherService.GetWeatherAsync(city);

            // If null, you may show an error in the view
            return View(model);
        }
    }
}
