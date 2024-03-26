using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }

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

        [Range(0,2)]
        public int RoleId { get; set; }
    }

    public class RegisterTutorDto : RegisterDto
    {
        [Required]
        [MaxLength(12)]
        public string? Cmnd { get; set; }

        public IFormFile? FrontCmnd { get; set; } 

        public IFormFile? BackCmnd { get; set; }

        public IFormFile? Cv { get; set; }

        [Required]
        public string? EducationLevel { get; set; }

        [Required]
        public string? School { get; set; }

        [Required]
        public string? GraduationYear { get; set; }

        [Required]
        public string? About { get; set; }
    }

    public class RegisterStaffDto : RegisterDto
    {
        [Required]
        public string? StaffType { get; set; }
    }
}
