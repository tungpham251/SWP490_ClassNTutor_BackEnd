using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Staff
{
    public long PersonId { get; set; }

    public string StaffType { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Person Person { get; set; } = null!;
}
