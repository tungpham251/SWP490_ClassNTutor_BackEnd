using Microsoft.AspNetCore.Mvc;
using SEP490_BackEnd.DTO.AccountProfile;

namespace SEP490_BackEnd.Services.Interface
{
    public interface IAccountServices
    {
        public Task<IActionResult> GetAccountByPersonId(int accountId);
        public Task<IActionResult> UpdateProfile(AccountProfileDTO accountProfileDTO);
    }
}
