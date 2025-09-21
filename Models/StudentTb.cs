using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class StudentTb
{
    public int StudentPkid { get; set; }

    public string? StudentId { get; set; }

    public string? FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Email { get; set; }
}
