using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModels
{
    public class Class_VM
    {
        public int ClassPkid { get; set; }

        [Required(ErrorMessage = "အတန်းအမည် ထည့်သွင်းရန် လိုအပ်ပါသည်")]
        [Display(Name = "အတန်းအမည်")]
        [StringLength(50)]
        public string? ClassName { get; set; }

        [Display(Name = "သင်နှစ်")]
        public int? Year { get; set; }

        [Display(Name = "အတန်းပိုင်ဆရာ")]
        public int? TeacherPkid { get; set; }

        [Display(Name = "အတန်းပိုင်ဆရာအမည်")]
        public string? TeacherName { get; set; }

        [Display(Name = "အပိုင်း")]
        [StringLength(10)]
        public string? Section { get; set; }

        [Display(Name = "ဖျက်ပြီးလား")]
        public bool? IsDeleted { get; set; }

        // Display properties
        public string DisplayName => $"{ClassName} - {Section} ({Year})";
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }

        // Audit fields
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class ClassDetail_VM : Class_VM
    {
        public List<StudentInfo_VM> Students { get; set; } = new();
        public List<CourseInfo_VM> Courses { get; set; } = new();
    }

    public class StudentInfo_VM
    {
        public int StudentPkid { get; set; }
        public string? StudentId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class CourseInfo_VM
    {
        public int CoursePkid { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public int? Credits { get; set; }
    }

    public class ClassSearch_VM
    {
        public string? ClassName { get; set; }
        public int? Year { get; set; }
        public string? Section { get; set; }
        public int? TeacherPkid { get; set; }
        public bool? ShowDeleted { get; set; } = false;
    }
}
 
