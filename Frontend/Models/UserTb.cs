using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventAPI.Models;

public partial class UserTb
{

    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPassword { get; set; }
    [JsonIgnore]
    public virtual ICollection<BookingTb> BookingTbs { get; set; } = new List<BookingTb>();
    [JsonIgnore]
    public virtual ICollection<TicketTb> TicketTbs { get; set; } = new List<TicketTb>();
}
