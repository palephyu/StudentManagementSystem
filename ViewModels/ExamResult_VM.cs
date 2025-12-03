using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModels
{
    public class ExamResult_VM
    {
        public int ResultPkid { get; set; }

        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }

        public int MarksObtained { get; set; }
        public string? Grade { get; set; }

        // Dropdowns
        public List<SelectListItem>? ExamList { get; set; }
        public List<SelectListItem>? StudentList { get; set; }
    }
}
