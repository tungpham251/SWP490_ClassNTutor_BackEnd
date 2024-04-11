using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class ProfileDto
    {
        public string FullName { get; set; } = null!;
        public string? UserAvatar { get; set; }
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public IEnumerable<SubjectTutorDto>? SubjectTutors { get; set; }
        public IEnumerable<StudentDto>? Students { get; set; }
        public GetTutorDto Tutor { get; set; }
        public GetStaffDto Staff { get; set; }
        public AccountDto Account { get; set; }
    }

    public class EditProfileDto
    {
        public string? FullName { get; set; }
        public IFormFile? Avatar { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateTime? Dob { get; set; }

        public EditTutorDto Tutor { get; set; }
        public EditStaffDto Staff { get; set; }
    }
}
