using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class EvaluationDto
    {
        public long? EvaluationId { get; set; }
        public long StudentId { get; set; }
        public long ClassId { get; set; }
        public string? StudentName { get; set; }
        public string? ClassName { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
    }

    public class AddEvaluationDto
    {

        public long StudentId { get; set; }
        public long ClassId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; } = null;
    }

    public class RequestEvaluationDto
    {
        [Range(0, long.MaxValue)]
        public long ClassId { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public long? StudentId { get; set; }
        public DateTime? Date { get; set; }
    }

    public class EvaluationForParentDto
    {
        [Range(0, long.MaxValue)]
        public long StudentId { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;

    }
}
