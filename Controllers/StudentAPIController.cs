using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using System;

namespace StudentManagementSystem.Controllers
{
    public class StudentAPIController : Controller
    {
        private readonly HttpClient _httpClient;
        private string apiBaseUrl = "https://localhost:7097/api/Student";
        public StudentAPIController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            //var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<StudentTb>>>(apiBaseUrl);
            //var students = response.Data;
            //return View(students);
            List<StudentTb> students = new List<StudentTb>();
            var response =  await _httpClient.GetAsync(apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<StudentTb>>>();
                students = apiResponse?.Data;
            }
            return View(students);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentTb student)
        {
            var response = await _httpClient.PostAsJsonAsync(
                apiBaseUrl, student);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Insert failed");
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentTb student)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7097/api/Student/{id}", student);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(student);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7097/api/Student/{id}");
            return RedirectToAction("Index");
        }



    }
}
