using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class SearchFilterPaymentDto
    {
        public int? PaymentAmount { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public long PayerId { get; set; }
        public long RequestId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
    public class ResponsePaymentDto
    {
        public long PaymentId { get; set; }
        public string PayerName { get; set; }
        public string RequestName { get; set; }
        public int PaymentAmount { get; set; }
        public string? PaymentDesc { get; set; }
        public string PaymentType { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

    }
    public class CreatePaymentDto
    {
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

    }
}
