using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class EventTb
{
    public int EventId { get; set; }

    public int? OrganizerId { get; set; }

    public string? EventName { get; set; }

    public string? EventType { get; set; }

    public string? EventArtist { get; set; }

    public string? EventThem { get; set; }

    public int? EventTicketPrice { get; set; }

    public string? EventMoreDetails { get; set; }

    public DateOnly? EventDate { get; set; }

    public string? EventImage { get; set; }

    public virtual ICollection<BookingRefundTb> BookingRefundTbs { get; set; } = new List<BookingRefundTb>();

    public virtual ICollection<BookingTb> BookingTbs { get; set; } = new List<BookingTb>();

    public virtual OrganizerTb? Organizer { get; set; }

    public virtual ICollection<TicketTb> TicketTbs { get; set; } = new List<TicketTb>();
}
