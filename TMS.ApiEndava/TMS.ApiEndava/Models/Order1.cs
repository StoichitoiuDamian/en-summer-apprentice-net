using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class Order1
{
    public long OrderId { get; set; }

    public int? NumberOfTickets { get; set; }

    public DateTime? OrderedAt { get; set; }

    public double? TotalPrice { get; set; }

    public long? TicketCategoryId { get; set; }

    public long? UserId { get; set; }

    public virtual TicketCategory? TicketCategory { get; set; }

    public virtual User1? User { get; set; }
}
