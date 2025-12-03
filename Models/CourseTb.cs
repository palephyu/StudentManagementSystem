using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class CourseTb
{
    public int CoursePkid { get; set; }

    public string? CourseCode { get; set; }

    public string? CourseName { get; set; }

    public string? Description { get; set; }

    public int? Credits { get; set; }
}
