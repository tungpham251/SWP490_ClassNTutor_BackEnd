using SEP490_BackEnd.Models;

namespace SEP490_BackEnd.ResponseModel.AccountProfile
{
    public class PersonResponse
    {
        public long PersonId { get; set; }

        public string FullName { get; set; } = null!;

        public string? UserAvatar { get; set; }

        public string Phone { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public virtual TutorResponse? Tutor { get; set; }
    }
}
