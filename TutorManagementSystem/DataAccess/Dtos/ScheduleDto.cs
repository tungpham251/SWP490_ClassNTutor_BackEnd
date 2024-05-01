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
        public DateTime? Date { get; set; }
        public string Status { get; set; } = null!;
    }
    public class ScheduleStudentDto
    {
        public long Id { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; } = null!;
        public IEnumerable<ScheduleStudentInformationDto> ScheduleStudentInformationDto { get; set; }
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
        [Required]
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public string Status { get; set; } = null!;

    }
    public class FilterScheduleDto
    {
        public long Id { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; } = null!;
        public long ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public string? StudentName { get; set; }
        public int? Attendent {  get; set; }
    }
}
