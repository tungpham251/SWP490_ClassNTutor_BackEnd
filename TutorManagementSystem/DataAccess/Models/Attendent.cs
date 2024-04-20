using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Attendent
    {
        public long ScheduleId { get; set; }
        public long StudentId { get; set; }
        public int? Attentdent { get; set; }

        public virtual Schedule Schedule { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
