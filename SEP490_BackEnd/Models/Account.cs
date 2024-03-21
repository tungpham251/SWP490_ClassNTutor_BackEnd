using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Account
{
    public long PersonId { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Person? Person { get; set; }

    public virtual Role Role { get; set; } = null!;
}
