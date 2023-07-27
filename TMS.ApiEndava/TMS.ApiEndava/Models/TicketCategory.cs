using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class TicketCategory
{
    public long TicketCategoryId { get; set; }

    public string? DescriptionTicketCategory { get; set; }

    public double? Price { get; set; }

    public long? EventId { get; set; }

    public virtual Event1? Event { get; set; }

    public virtual ICollection<Order1> Order1s { get; set; } = new List<Order1>();
}
