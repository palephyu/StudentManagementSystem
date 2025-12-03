using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models;

public partial class UserTb
{
    public int UserPkid { get; set; }

    public string? Username { get; set; }

    public string? UserType { get; set; }

    public int? Password { get; set; }
}
