using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class AttendanceTb
{
    public int AttendancePkid { get; set; }

    public int? StudentPkid { get; set; }

    public int? CoursePkid { get; set; }

    public int? ClassPkid { get; set; }

    public DateOnly? Date { get; set; }

    public bool? IsPresent { get; set; }

    public string? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
