using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class AdminTb
{
    public int AdminId { get; set; }

    public string? AdminEmail { get; set; }

    public string? AdminPassword { get; set; }
}
