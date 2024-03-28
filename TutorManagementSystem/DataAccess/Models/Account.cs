using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            RequestParents = new HashSet<Request>();
            RequestTutors = new HashSet<Request>();
        }

        public long PersonId { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Request> RequestParents { get; set; }
        public virtual ICollection<Request> RequestTutors { get; set; }
    }
}
