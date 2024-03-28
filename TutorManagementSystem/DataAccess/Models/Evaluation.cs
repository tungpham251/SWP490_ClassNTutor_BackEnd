using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Evaluation
    {
        public long EvaluationId { get; set; }
        public long StudentId { get; set; }
        public long ClassId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
