using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class ClassStudentsTb
{
    public int ClassStudentPkid { get; set; }

    public int? ClassPkid { get; set; }

    public int? StudentPkid { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public bool? IsActive { get; set; }
}
