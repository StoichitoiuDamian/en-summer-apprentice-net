using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class Event1
{
    public long EventId { get; set; }

    public string? DescriptionEvent { get; set; }

    public DateTime? EndDate { get; set; }

    public string? EventName { get; set; }

    public DateTime? StartDate { get; set; }

    public long? EventTypeId { get; set; }

    public long? VenueId { get; set; }

    public virtual EventType? EventType { get; set; }

    public virtual ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();

    public virtual Venue? Venue { get; set; }
}
