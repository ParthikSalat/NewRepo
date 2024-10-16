using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class BookingRefundTb
{
    public int RefundId { get; set; }

    public int? BookingId { get; set; }

    public int? EventId { get; set; }

    public int? EventTicketPrice { get; set; }

    public int? NumberOfTicketCancelled { get; set; }

    public DateTime? CancleDate { get; set; }
    [JsonIgnore]
    public virtual BookingTb? Booking { get; set; }
    [JsonIgnore]
    public virtual EventTb? Event { get; set; }
}
