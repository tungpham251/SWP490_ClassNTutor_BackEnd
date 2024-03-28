using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class ClassDto
    {
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public long Price { get; set; }
        public string? SubjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
    }

    public class AddClassDto
    {
        public long TutorId { get; set; }
        [Required]
        public string? ClassName { get; set; }
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public long Price { get; set; }
        public long SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public string? Status { get; set; }
    }

    public class ClassRequestDto
    {
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }
}
