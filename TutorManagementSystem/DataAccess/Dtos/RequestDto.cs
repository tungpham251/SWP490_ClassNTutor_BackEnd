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
        public string? StudentName { get; set; }
        public string? TutorName { get; set; }
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

    public class UpdateRequestDto
    {
        public long RequestId { get; set; }
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
        [Range(0, long.MaxValue)]
        public long PersonId { get; set; }
        [Range(0, long.MaxValue)]
        public long SubjectId { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }

    
}
