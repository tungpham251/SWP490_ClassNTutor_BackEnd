using DataAccess.Dtos;
using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetAccountId(string token);
        Task<LoginResponseDto> Login(LoginDto entity);

        Task<bool> ResetPassword(string email);

        Task<bool> ChangePassword(ChangePasswordDto entity);

        Task<bool> RegisterParent(RegisterDto entity);

        Task<bool> RegisterStaff(RegisterStaffDto entity);

        Task<bool> RegisterTutor(RegisterTutorDto entity);
        public Task<bool> SuspendAccount(long id);

    }
}
