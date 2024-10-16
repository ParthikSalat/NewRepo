using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public virtual ICollection<BookingRefundTb> BookingRefundTbs { get; set; } = new List<BookingRefundTb>();
    [JsonIgnore]
    public virtual EventTb? Event { get; set; }
    [JsonIgnore]
    public virtual ICollection<PaymentTb> PaymentTbs { get; set; } = new List<PaymentTb>();
    [JsonIgnore]
    public virtual ICollection<TicketTb> TicketTbs { get; set; } = new List<TicketTb>();
    [JsonIgnore]
    public virtual UserTb? User { get; set; }
}
