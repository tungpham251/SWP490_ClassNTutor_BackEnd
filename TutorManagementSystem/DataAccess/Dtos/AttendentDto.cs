using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class AttendentDto
    {
        public long ScheduleId { get; set; }
        public long StudentId { get; set; }
        public int? Attentdent { get; set; }
        public string? FullName { get; set; }
        public string? UserAvatar { get; set; }
    }
    public class AttendentRequestDto
    {
        public long ScheduleId { get; set; }
        public long StudentId { get; set; }
        public int? Attentdent { get; set; }
    }

}
