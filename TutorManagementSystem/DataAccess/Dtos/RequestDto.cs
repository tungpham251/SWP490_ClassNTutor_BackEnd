using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class RequestDto
    {
        public long RequestId { get; set; }
        public string? ParentName { get; set; }
        public long? StudentId { get; set; }
        public string? RequestType { get; set; } 
        public string? ClassName { get; set; }
        public int? Level { get; set; }
        public long? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public long? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
    }

    public class AddRequestDto
    {
        public long ParentId { get; set; }
        public long TutorId { get; set; }
        public long StudentId { get; set; }
        public string? RequestType { get; set; }
        public long? ClassId { get; set; }
        public int? Level { get; set; }
        public long SubjectId { get; set; }
        public long? Price { get; set; }
        public string? Status { get; set; } 
    }

    public class RequestRequestDto
    {
        public int SubjectId { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
    }

    public class RequestOfTutorRequestDto
    {
        public int TutorId { get; set; }
        public int SubjectId { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
    }
}
