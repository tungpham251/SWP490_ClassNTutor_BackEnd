using Microsoft.AspNetCore.Http;
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

    public class GetTutorDto
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
        public string? FullName { get; set; }
        public string? UserAvatar { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }

    }

    public class EditTutorDto
    {
        public string? Cmnd { get; set; }
        public IFormFile? FrontCmnd { get; set; }
        public IFormFile? BackCmnd { get; set; }
        public IFormFile? Cv { get; set; }

        public string? EducationLevel { get; set; }
        public string? School { get; set; }
        public string? GraduationYear { get; set; }
        public string? About { get; set; }

    }

    public class GetStaffDto
    {
        public long PersonId { get; set; }
        public string StaffType { get; set; } = null!;
    }
    public class EditStaffDto
    {
        public string? StaffType { get; set; }
    }

}
