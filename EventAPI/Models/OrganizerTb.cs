using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class OrganizerTb
{
    public int OrganizerId { get; set; }

    public string? OrganizerName { get; set; }

    public string? OrganizerEmail { get; set; }

    public string? OrganizerMobileNo { get; set; }

    public string? OrganizerPassword { get; set; }

    public virtual ICollection<EventTb> EventTbs { get; set; } = new List<EventTb>();
}
