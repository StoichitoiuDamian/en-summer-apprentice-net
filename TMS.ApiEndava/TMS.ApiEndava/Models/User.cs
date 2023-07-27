using System;
using System.Collections.Generic;

namespace TMS.ApiEndava.Models;

public partial class User
{
    public long UserId { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Order> Order1s { get; set; } = new List<Order>();
}
