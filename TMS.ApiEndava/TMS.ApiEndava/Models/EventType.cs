using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class EventType
{
    public long EventTypeId { get; set; }

    public string? EventTypeName { get; set; }

    public virtual ICollection<Event1> Event1s { get; set; } = new List<Event1>();
}
