using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [Authorize(Roles = "STAFF")]
        [HttpGet("get-classes")]
        public async Task<IActionResult> GetClasses([FromQuery] ClassRequestDto entity)
        {
            var result = await _classService.GetClasses(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpGet("get-class-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _classService.GetById(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpPost("add-class")]
        public async Task<IActionResult> AddClass([FromForm] AddClassDto entity)
        {
            var result = await _classService.AddClass(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpGet("get-classes-of-tutor")]
        public async Task<IActionResult> GetClassesOfTutor([FromQuery] ClassOfTutorRequestDto entity)
        {
            var result = await _classService.GetClassesOfTutor(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

    }
}
