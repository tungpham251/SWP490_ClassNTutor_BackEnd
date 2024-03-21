using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_BackEnd.DTO.AccountProfile;
using SEP490_BackEnd.Services.Interface;
using SEP490_BackEnd.Ultils;

namespace SEP490_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountRepository;

        public AccountController(IAccountServices accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("get-account-by-personId")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            IActionResult ResponseApi = await _accountRepository.GetAccountByPersonId(accountId);
            return ResponseApi;
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] AccountProfileDTO accountProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors =  ValidationModel.GetValidationErrors(ModelState);
                return new ObjectResult(validationErrors)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            IActionResult ResponseApi = await _accountRepository.UpdateProfile(accountProfileDTO);
            return ResponseApi;
        }
    }
}
