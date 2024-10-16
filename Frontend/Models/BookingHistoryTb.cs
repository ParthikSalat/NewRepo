using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class BookingHistoryTb
{
    public int BookingHistoryId { get; set; }

    public int? BookingId { get; set; }

    public int? BookingAmount { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? NumberOfTicket { get; set; }
}
