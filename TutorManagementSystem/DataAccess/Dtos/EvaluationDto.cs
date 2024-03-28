using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class EvaluationDto
    {
        public long EvaluationId { get; set; }
        public long StudentId { get; set; }
        public long ClassId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; }
    }
}
