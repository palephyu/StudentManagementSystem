using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class ClassTb
{
    public int Classpkid { get; set; }

    public int? StudentPkid { get; set; }

    public string? ClassName { get; set; }

    public int? Year { get; set; }

    public int? TeacherPkid { get; set; }
}
