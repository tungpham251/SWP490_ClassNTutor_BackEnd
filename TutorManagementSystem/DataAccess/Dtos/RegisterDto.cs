using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string? FullName { get; set; }

        public IFormFile? Avatar { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateTime? Dob { get; set; }

        [Range(0, 2, ErrorMessage = "Invalid role ID")]
        public int RoleId { get; set; }
    }

    public class RegisterTutorDto : RegisterDto
    {
        [Required(ErrorMessage = "CMND is required")]
        [MaxLength(12, ErrorMessage = "CMND cannot exceed 12 characters")]
        public string? Cmnd { get; set; }

        [Required(ErrorMessage = "Front CMND image is required")]
        public IFormFile? FrontCmnd { get; set; }

        [Required(ErrorMessage = "Back CMND image is required")]
        public IFormFile? BackCmnd { get; set; }

        [Required(ErrorMessage = "CV file is required")]
        public IFormFile? Cv { get; set; }

        [Required(ErrorMessage = "Education level is required")]
        public string? EducationLevel { get; set; }

        [Required(ErrorMessage = "School name is required")]
        public string? School { get; set; }

        [Required(ErrorMessage = "Graduation year is required")]
        public string? GraduationYear { get; set; }
       
        public string? About { get; set; }
    }

    public class RegisterStaffDto : RegisterDto
    {
        [Required(ErrorMessage = "Staff type is required")]
        public string? StaffType { get; set; }
    }

}