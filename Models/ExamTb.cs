using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class ExamTb
{
    public int ExamPkid { get; set; }

    public int? CoursePkid { get; set; }

    public int? ClassPkid { get; set; }

    public string? ExamTitle { get; set; }

    public DateOnly? ExamDate { get; set; }

    public int? MaxMarks { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Location { get; set; }
}
