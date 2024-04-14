using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutorService;


        public TutorController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        //[Authorize(Roles = "STAFF")]
        [HttpGet("get-tutors-active-by-subject-name")]
        public async Task<IActionResult> GetAllTutorActive([FromQuery] TutorRequestDto entity)
        {
            var result = await _tutorService.GetAllTutorActive(entity).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }


    }
}
