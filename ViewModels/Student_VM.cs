using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModels
{
    public class Student_VM
    {
        public int StudentPkid { get; set; }

        public string? StudentId { get; set; }

        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; } = null;
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? ImagePath { get; set; }

        // Relations
        public int Classpkid { get; set; }

        public string? ClassName { get; set; }

        // Dropdown
        public List<SelectListItem>? ClassList { get; set; }
    }
}
