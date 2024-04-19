using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
    public class LoginDto
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string? Token { get; set; }

        public long? UserId { get; set; }

        public string? RoleName { get; set; }
        public string? FullName { get; set; }
        public string? UserAvatar { get; set; }

        public LoginResponseDto(string? token, long? userId, string? roleName, string? fullName, string? avatar)
        {
            Token = token;
            UserId = userId;
            RoleName = roleName;
            FullName = fullName;
            UserAvatar = avatar;
        }
    }
}
