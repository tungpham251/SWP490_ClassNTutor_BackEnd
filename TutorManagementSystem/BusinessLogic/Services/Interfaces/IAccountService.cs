using DataAccess.Dtos;
using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetAccountId(string token);
        Task<string> Login(LoginDto entity);

        Task<bool> ResetPassword(string email);

        Task<bool> ChangePassword(ChangePasswordDto entity);

        Task<bool> RegisterParent(RegisterDto entity);

        Task<bool> RegisterStaff(RegisterStaffDto entity);

        Task<bool> RegisterTutor(RegisterTutorDto entity);

        Task<IEnumerable<PersonDto>> GetAllParents();

        Task<IEnumerable<PersonDto>> GetAllTutors();

        Task<IEnumerable<PersonDto>> GetAllStudents();
    }
}
