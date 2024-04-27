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

        
        [HttpGet("get-classes")]
        public async Task<IActionResult> GetClasses([FromQuery] ClassRequestDto entity)
        {
            var result = await _classService.GetClasses(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpGet("get-class-for-tutor")]
        public async Task<IActionResult> GetClassForTutor([FromQuery] ClassForTutorRequestDto entity)
        {
            var result = await _classService.GetClassesForTutor(entity).ConfigureAwait(false);
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
        [HttpPut("suspend-class/{classId}")]
        public async Task<IActionResult> DeleteClassById([FromRoute] long classId)
        {
            var result = await _classService.DeleteClassById(classId).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }        

        [Authorize(Roles = "PARENT")]
        [HttpGet("get-class-for-parent")]
        public async Task<IActionResult> GetClassForParent([FromQuery] ClassForParentRequestDto entity)
        {
            var result = await _classService.GetClassesForParent(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpPost("add-students-in-class")]
        public async Task<IActionResult> AddStudentsInClass([FromBody] List<AddStudentInClassRequestDto> entity)
        {
            var result = await _classService.AddStudentsInClass(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpPost("delete-student-in-class")]
        public async Task<IActionResult> DeleteStudentInClass([FromBody] DeleteStudentInClassRequestDto entity)
        {
            var result = await _classService.DeleteStudentInClass(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpGet("get-students-in-class")]
        public async Task<IActionResult> GetStudentsInClass([FromQuery] StudentInClassRequestDto entity)
        {
            var result = await _classService.GetStudentsInClass(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR,PARENT")]
        [HttpGet("get-class-details/{id}")]
        public async Task<IActionResult> GetClassDetail([FromRoute] long id)
        {
            var result = await _classService.GetClassByIdIncludeStudentInformation(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpPost("add-class")]
        public async Task<IActionResult> AddClassInculdeSchedule([FromBody] AddClassIncludeScheduleDto entity)
        {
            var result = await _classService.AddClassIncludeSchedule(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpPut("update-class")]
        public async Task<IActionResult> UpdateClassIncludeSchedule([FromBody] UpdateClassIncludeScheduleDto entity)
        {
            var result = await _classService.UpdateClassIncludeSchedule(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
