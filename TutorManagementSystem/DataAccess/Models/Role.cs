using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDesc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
