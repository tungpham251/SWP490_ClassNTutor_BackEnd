using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "ADMIN,STAFF,TUTOR, PARENT")]
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfileByCurrentUser()
        {

            string personId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(personId))
                return BadRequest(new ApiFormatResponse(StatusCodes.Status404NotFound, false, "Login pls"));

            var result = await _personService.GetProfileByCurrentUser(personId).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            }
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }


        [Authorize(Roles = "ADMIN,STAFF,TUTOR, PARENT")]
        [HttpPut("edit-profile")]
        public async Task<IActionResult> EditProfileCurrentUser([FromForm] EditProfileDto entity)
        {

            string personId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(personId))
                return BadRequest(new ApiFormatResponse(StatusCodes.Status404NotFound, false, "Login pls"));

            var result = await _personService.EditProfileCurrentUser(entity, personId).ConfigureAwait(false);
            if (result == false)
            {
                return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            }
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
