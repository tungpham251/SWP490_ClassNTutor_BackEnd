using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [HttpGet("get-account-id")]
        public async Task<IActionResult> GetAccountId()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token)) BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, token));

            var id = await _accountService.GetAccountId(token!).ConfigureAwait(false);

            if (string.IsNullOrEmpty(id)) BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, id));

            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, id));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto entity)
        {
            var token = await _accountService.Login(entity).ConfigureAwait(false);
            if (string.IsNullOrEmpty(token)) return Unauthorized(new ApiFormatResponse(StatusCodes.Status401Unauthorized, false, token));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, token));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        [HttpPost("register-tutor")]
        public async Task<IActionResult> RegisterTutor([FromForm] RegisterTutorDto entity)
        {
            var result = await _accountService.RegisterTutor(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpPost("register-staff")]
        public async Task<IActionResult> RegisterStaff([FromForm] RegisterStaffDto entity)
        {
            var result = await _accountService.RegisterStaff(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpPost("register-parent")]
        public async Task<IActionResult> RegisterParent([FromForm] RegisterDto entity)
        {
            var result = await _accountService.RegisterParent(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] string email)
        {
            var result = await _accountService.ResetPassword(email).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDto entity)
        {
            var result = await _accountService.ChangePassword(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpGet("get-all-tutors")]
        public async Task<IActionResult> GetAllTutors()
        {
            var result = await _accountService.GetAllTutors().ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpGet("get-all-parents")]
        public async Task<IActionResult> GetAllParents()
        {
            var result = await _accountService.GetAllParents().ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _accountService.GetAllStudents().ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
