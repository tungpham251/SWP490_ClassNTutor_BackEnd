using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class ClassDto
    {
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? TutorName { get; set; }
        public long? TutorId { get; set; }
        public string UserAvatar { get; set; } = null!;
        public string EducationLevel { get; set; } = null!;
        public string School { get; set; } = null!;
        public string GraduationYear { get; set; } = null!;
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public int NumOfSession { get; set; }
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
        [Range(0, long.MaxValue)]
        public long TutorId { get; set; }
        [Required]
        public string? ClassName { get; set; }
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public int NumOfSession {  get; set; }
        public long Price { get; set; }
        [Range(0, long.MaxValue)]
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
        public long? SubjectId { get; set; }
    }

    public class ClassForTutorRequestDto
    {
        [Range(0, long.MaxValue)]
        public long TutorId { get; set; }
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }

    public class ClassForParentRequestDto
    {
        [Range(0, long.MaxValue)]
        public long ParentId { get; set; }
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }

    public class UpdateClassDto
    {
        [Range(0, long.MaxValue)]
        public long ClassId { get; set; }
        [Range(0, long.MaxValue)]
        public long TutorId { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public long Price { get; set; }
        [Range(0, long.MaxValue)]
        public long SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
    }

    public class ClassDetailsIncludeStudentInfoDto
    {
        public long ClassId { get; set; }
        public long TutorId { get; set; }
        public string TutorName { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public int NumOfSession { get; set; }
        public long Price { get; set; }
        public string SubjectName { get; set; }
        public long SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;
        public IEnumerable<StudentInformationDto> StudentInformationDto { get; set; }
        public IEnumerable<ScheduleDto>? Schedules { get; set; }

    }
    public class ClassDetailsIncludeScheduleStudentDto
    {
        public long ClassId { get; set; }
        public long TutorId { get; set; }
        public string TutorName { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public int NumOfSession { get; set; }
        public long Price { get; set; }
        public string SubjectName { get; set; }
        public long SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;
        public IEnumerable<ScheduleStudentDto>? Schedules { get; set; }

    }
    public class ScheduleStudentInformationDto
    {
        public string FullName { get; set; } = null!;
        public int? Attentdent { get; set; }
    }
    public class StudentInformationDto
    {   
        public long StudentId { get; set; }
        public string FullName { get; set; } = null!;
        public string? UserAvatar { get; set; }
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ParentName { get; set; } = null!;
        public string ParentPhone { get; set; } = null!;
        public int StudentLevel { get; set; }
        public string StatusClassMember { get; set; } = null!;

    }

    public class AddClassIncludeScheduleDto
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
        public int? NumOfSession { get; set; }
        public IEnumerable<AddScheduleDto>? AddScheduleDto { get; set; }
    }

    public class UpdateClassIncludeScheduleDto
    {
        [Required]
        public long ClassId { get; set; }
        public long TutorId { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public long Price { get; set; }
        public long SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public IEnumerable<UpdateScheduleDto>? UpdateScheduleDto { get; set; }

    }
}
