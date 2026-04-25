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

    public string? Section { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
