using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendentController : ControllerBase
    {
        private readonly IAttendentService _attendentService;


        public AttendentController(IAttendentService attendentService)
        {
            _attendentService = attendentService;
        }

        [HttpGet("get-attend-students")]
        public async Task<IActionResult> GetStudentsAttend([FromQuery] long scheduleId)
        {
            var result =await _attendentService.GetStudentsAttend(scheduleId).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpPut("update-attend-students")]
        public async Task<IActionResult> UpdateStudentsAttend([FromBody] List<AttendentRequestDto> entity)
        {
            var result = await _attendentService.UpdateStudentsAttend(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpPut("check-schedule-class")]
        public async Task<IActionResult> CheckScheduleClass()
        {
            string tutorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(tutorId))
                return BadRequest(new ApiFormatResponse(StatusCodes.Status404NotFound, false, "Login pls"));

            var result = await _attendentService.CheckScheduleClass(long.Parse(tutorId)).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

    }
}
