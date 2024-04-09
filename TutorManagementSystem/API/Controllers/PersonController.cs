using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [Authorize(Roles = "STAFF")]
        [HttpGet("get-staffs")]
        public async Task<IActionResult> GetStaffs([FromQuery] PersonRequestDto entity)
        {
            var result = await _personService.GetStaffs(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccounts([FromQuery] PersonRequestDto entity)
        {
            var result = await _personService.GetAccounts(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
