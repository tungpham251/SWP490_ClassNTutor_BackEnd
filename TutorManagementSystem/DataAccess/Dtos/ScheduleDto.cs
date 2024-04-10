using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class ScheduleDto
    {
        public long Id { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public string Status { get; set; } = null!;
    }
    public class AddScheduleDto
    {
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public string Status { get; set; } = null!;
    }
    public class UpdateScheduleDto
    {
        public long Id { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public string Status { get; set; } = null!;

    }
}
