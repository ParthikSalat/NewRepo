using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventAPI.Models;

public partial class TicketTb
{
    public int TicketId { get; set; }

    public int? BookingId { get; set; }

    public int? UserId { get; set; }

    public int? EventId { get; set; }

    public int? PaymentId { get; set; }
    [JsonIgnore]
    public virtual BookingTb? Booking { get; set; }
    [JsonIgnore]
    public virtual EventTb? Event { get; set; }
    [JsonIgnore]
    public virtual PaymentTb? Payment { get; set; }
    [JsonIgnore]
    public virtual UserTb? User { get; set; }
}
