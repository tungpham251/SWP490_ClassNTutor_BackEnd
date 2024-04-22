using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class SubjectDto
    {
        public long SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Status { get; set; }
    }

    public class SubjectRequestDto
    {
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }

    public class SubjectOfTutorDto
    {
        public long SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Status { get; set; }
        public int Level { get; set; }
    }
}
