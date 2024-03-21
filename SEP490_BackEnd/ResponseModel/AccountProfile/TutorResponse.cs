using SEP490_BackEnd.Models;

namespace SEP490_BackEnd.ResponseModel.AccountProfile
{
    public class TutorResponse
    {
        public long PersonId { get; set; }

        public string Cmnd { get; set; } = null!;

        public string FrontCmnd { get; set; } = null!;

        public string BackCmnd { get; set; } = null!;

        public string Cv { get; set; } = null!;

        public string EducationLevel { get; set; } = null!;

        public string School { get; set; } = null!;

        public string GraduationYear { get; set; } = null!;

        public string? About { get; set; }
    }
}
