using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class EnrollmentTb
{
    public int EnrollmentPkid { get; set; }

    public int? CoursePkid { get; set; }

    public int? StudentPkid { get; set; }

    public DateOnly? EnrolledDate { get; set; }
}
