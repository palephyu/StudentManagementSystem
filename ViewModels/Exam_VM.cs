using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModels
{
    public class Exam_VM
    {
        public int ExamPkid { get; set; }

        public int? CoursePkid { get; set; }
        public int? ClassPkid { get; set; }
        public string? CourseName {  get; set; }
        public string? ClassName { get; set; }

        public string? ExamTitle { get; set; }
        public DateOnly? ExamDate { get; set; }

        public int? MaxMarks { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string? Location { get; set; }

        public List<SelectListItem>? CourseList { get; set; }
    }
}
