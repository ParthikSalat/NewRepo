using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class BookingTb
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? EventId { get; set; }

    public int? RemainingTicket { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? BookingAmount { get; set; }

    public string? BookingStatus { get; set; }

    public string? NumberOfTicket { get; set; }

    public virtual ICollection<BookingRefundTb> BookingRefundTbs { get; set; } = new List<BookingRefundTb>();

    public virtual EventTb? Event { get; set; }

    public virtual ICollection<PaymentTb> PaymentTbs { get; set; } = new List<PaymentTb>();

    public virtual ICollection<TicketTb> TicketTbs { get; set; } = new List<TicketTb>();

    public virtual UserTb? User { get; set; }
}
