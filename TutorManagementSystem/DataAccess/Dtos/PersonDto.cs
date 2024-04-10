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
        public string? FullName { get; set; }
        public string? UserAvatar { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; } 
        public string? Address { get; set; } 
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public string? Status { get; set; }
    }

    public class PersonRequestDto
    {
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
        public string? Status { get; set; }
    }
}
