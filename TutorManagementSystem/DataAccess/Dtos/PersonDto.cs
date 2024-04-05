using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class PersonDto
    {
        public long PersonId { get; set; }
        public string FullName { get; set; } = null!;
        public string? UserAvatar { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; } 
        public string? Address { get; set; } 
        public DateTime? Dob { get; set; }
        public string Email { get; set; } = null!;
    }
}
