using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class UserTb
{
    public int UserPkid { get; set; }

    public string? Username { get; set; }

    public string? UserType { get; set; }

    public int? Password { get; set; }

    public string? Role { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
