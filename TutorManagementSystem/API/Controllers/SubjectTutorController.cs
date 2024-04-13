using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectTutorController : ControllerBase
    {
        private readonly ISubjectTutorService _subjectTutorService;


        public SubjectTutorController(ISubjectTutorService subjectTutorService)
        {
            _subjectTutorService = subjectTutorService;
        }

        //[Authorize(Roles = "STAFF")]
        [HttpPost("add-subject-tutor")]
        public async Task<IActionResult> AddSubjectTutor([FromBody] AddSubjectTutorDto entity)
        {
            var result = await _subjectTutorService.AddSubjectTutor(entity).ConfigureAwait(false);
            if (result == false) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        //[Authorize(Roles = "STAFF")]
        [HttpDelete("delete-subject-tutor/{tutorId}/{subjectId}")]
        public async Task<IActionResult> DeleteSubjectTutor([FromRoute] long tutorId, long subjectId)
        {
            var result = await _subjectTutorService.DeleteSubjectTutor(tutorId, subjectId).ConfigureAwait(false);
            if (result == false) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
