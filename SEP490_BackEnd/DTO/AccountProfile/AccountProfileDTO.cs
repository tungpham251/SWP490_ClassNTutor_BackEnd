using SEP490_BackEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace SEP490_BackEnd.DTO.AccountProfile
{
    public class AccountProfileDTO
    {
        [Required(ErrorMessage = "Vui lòng điền PersonId")]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        public string FullName { get; set; } = null!;

        public string? UserAvatar { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập giới tính!")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số căn cước")]
        public string Cmnd { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn ảnh mặt trước")]
        public string FrontCmnd { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn ảnh mặt sau")]
        public string BackCmnd { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn ảnh CV")]
        public string Cv { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập trình độ học vấn")]
        public string EducationLevel { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập trường học")]
        public string School { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập năm tốt nghiệp")]
        public string GraduationYear { get; set; } = null!;
        public string? About { get; set; }
    }
}
