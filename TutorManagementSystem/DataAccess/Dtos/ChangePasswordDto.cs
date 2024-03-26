using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? OldPassword { get; set; }

        [Required]
        public string? NewPassword { get; set; }

        [Required]
        public string? RePassword { get; set; }
    }
}
