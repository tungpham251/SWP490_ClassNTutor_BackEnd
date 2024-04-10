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
        public IEnumerable<StudentDto>? Students{ get; set; }
    }

    public class EditProfileDto
    {
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }
        public IFormFile? Avatar { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }

        [Required]
        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateTime? Dob { get; set; }
    }
}
