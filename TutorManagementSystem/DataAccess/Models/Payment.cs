using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Payment
    {
        public long PaymentId { get; set; }
        public long PayerId { get; set; }
        public long RequestId { get; set; }
        public int PaymentAmount { get; set; }
        public string? PaymentDesc { get; set; }
        public string PaymentType { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Tutor Payer { get; set; } = null!;
        public virtual Staff Request { get; set; } = null!;
    }
}
