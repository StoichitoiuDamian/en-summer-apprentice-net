using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class User1
{
    public long UserId { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Order1> Order1s { get; set; } = new List<Order1>();
}
