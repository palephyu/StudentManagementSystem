using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class TeacherTb
{
    public int TeacherPkid { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? Specialization { get; set; }
}
