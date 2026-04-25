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

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
