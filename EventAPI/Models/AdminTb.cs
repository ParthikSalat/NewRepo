using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class AdminTb
{
    public int Adminid { get; set; }

    public string AdminEmail { get; set; } = null!;

    public string AdminPassword { get; set; } = null!;
}
