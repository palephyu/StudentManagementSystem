using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModels
{
    public class Exam_VM
    {
        public int ExamPkid { get; set; }
        public string? ExamTitle { get; set; }
        public DateTime ExamDate { get; set; }
        public int MaxMarks { get; set; }

        public int CoursePkid { get; set; }
        public string? CourseName { get; set; }

        public List<SelectListItem>? CourseList { get; set; }
    }
}
