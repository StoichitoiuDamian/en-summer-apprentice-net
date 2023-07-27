using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class Venue
{
    public long VenueId { get; set; }

    public int? VenueCapacity { get; set; }

    public string? VenueLocation { get; set; }

    public string? VenueType { get; set; }

    public virtual ICollection<Event1> Event1s { get; set; } = new List<Event1>();
}
