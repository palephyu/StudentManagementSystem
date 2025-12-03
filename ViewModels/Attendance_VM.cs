using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModels
{
    public class Attendance_VM
    {
        public int AttendanceId { get; set; }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public DateTime Date { get; set; }
        public string? Status { get; set; } // Present/Absent/Leave

        // Dropdowns
        public List<SelectListItem>? StudentList { get; set; }
        public List<SelectListItem>? CourseList { get; set; }
    }
}
