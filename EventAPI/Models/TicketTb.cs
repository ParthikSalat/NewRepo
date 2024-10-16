using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class TicketTb
{
    public int TicketId { get; set; }

    public int? BookingId { get; set; }

    public int? UserId { get; set; }

    public int? EventId { get; set; }

    public int? PaymentId { get; set; }

    public virtual BookingTb? Booking { get; set; }

    public virtual EventTb? Event { get; set; }

    public virtual PaymentTb? Payment { get; set; }

    public virtual UserTb? User { get; set; }
}
