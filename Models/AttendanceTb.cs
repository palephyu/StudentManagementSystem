using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class AttendanceTb
{
    public int AttendancePkid { get; set; }

    public int? StudentPkid { get; set; }

    public int? CoursePkid { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }
}
