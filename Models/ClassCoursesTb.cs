using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class ClassCoursesTb
{
    public int ClassCoursePkid { get; set; }

    public int? ClassPkid { get; set; }

    public int? CoursePkid { get; set; }

    public DateTime? AssignedDate { get; set; }

    public int? TeacherPkid { get; set; }

    public bool? IsActive { get; set; }
}
