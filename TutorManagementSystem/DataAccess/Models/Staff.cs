using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Payments = new HashSet<Payment>();
        }

        public long PersonId { get; set; }
        public string StaffType { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
