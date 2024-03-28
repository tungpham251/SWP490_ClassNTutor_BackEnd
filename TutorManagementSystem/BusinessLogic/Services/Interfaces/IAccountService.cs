using DataAccess.Dtos;
using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetEmail(string token);
        Task<string> Login(LoginDto entity);

        Task<bool> ResetPassword(string email);

        Task<bool> ChangePassword(ChangePasswordDto entity);

        Task<bool> RegisterParent(RegisterDto entity);

        Task<bool> RegisterStaff(RegisterStaffDto entity);

        Task<bool> RegisterTutor(RegisterTutorDto entity);

        Task<Account> AddAccount(RegisterDto entity);

        Task<Person> AddPerson(RegisterDto entity);
    }
}
