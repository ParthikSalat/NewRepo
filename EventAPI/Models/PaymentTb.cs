using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class PaymentTb
{
    public int PaymentId { get; set; }

    public int? BookingId { get; set; }

    public int? TotalAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual BookingTb? Booking { get; set; }

    public virtual ICollection<TicketTb> TicketTbs { get; set; } = new List<TicketTb>();
}
