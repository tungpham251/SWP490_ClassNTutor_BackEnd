using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;


        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "PARENT")]
        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentDto entity)
        {
            var result = await _studentService.AddStudent(entity).ConfigureAwait(false);
            if (result == false) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "PARENT")]
        [HttpPut("update-student")]
        public async Task<IActionResult> UpdateStudent([FromQuery] UpdateStudentDto entity)
        {
            var result = await _studentService.UpdateStudent(entity).ConfigureAwait(false);
            if (result == false) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "PARENT")]
        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] long id)
        {
            var result = await _studentService.DeleteStudent(id).ConfigureAwait(false);
            if (result == false) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
