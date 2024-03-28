using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Schedule
    {
        public long Id { get; set; }
        public long ClassId { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public string Status { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
    }
}
