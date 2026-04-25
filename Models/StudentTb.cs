using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class StudentTb
{
    public int StudentPkid { get; set; }

    public string? StudentId { get; set; }

    public string? FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public string? ImagePath { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ClassPkid { get; set; }
}
