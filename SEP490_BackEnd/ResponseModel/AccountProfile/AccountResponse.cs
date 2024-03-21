using SEP490_BackEnd.Models;

namespace SEP490_BackEnd.ResponseModel.AccountProfile
{
    public class AccountResponse
    {
        public long PersonId { get; set; }

        public int RoleId { get; set; }

        public string Email { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Status { get; set; } = null!;

        public virtual PersonResponse? Person { get; set; }
    }
}
