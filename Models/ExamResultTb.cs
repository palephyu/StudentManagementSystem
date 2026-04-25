using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class ExamResultTb
{
    public int ResultPkid { get; set; }

    public int? ExamPkid { get; set; }

    public int? StudentPkid { get; set; }

    public int? ClassPkid { get; set; }

    public int? CoursePkid { get; set; }

    public int? MarksObtained { get; set; }

    public string? Grade { get; set; }

    public string? Remarks { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
