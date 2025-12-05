namespace StudentManagementSystem.ViewModels
{
    public class Teacher_VM
    {
        public int TeacherPkid { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly HireDate { get; set; }
        public string? Specialization { get; set; }
    }
}
