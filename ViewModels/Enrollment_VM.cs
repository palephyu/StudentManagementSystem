using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModels
{
    public class Enrollment_VM
    {
        public int EnrollmentPkid { get; set; }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }

        public int CoursePkid { get; set; }
        public string? CourseName { get; set; }

        public DateTime EnrolledDate { get; set; }

        // Dropdowns
        public List<SelectListItem>? StudentList { get; set; }
        public List<SelectListItem>? CourseList { get; set; }
    }
}
