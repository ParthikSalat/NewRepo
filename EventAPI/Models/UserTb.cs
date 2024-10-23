using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class UserTb
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPassword { get; set; }

    public virtual ICollection<BookingTb> BookingTbs { get; set; } = new List<BookingTb>();

    public virtual ICollection<TicketTb> TicketTbs { get; set; } = new List<TicketTb>();
}
