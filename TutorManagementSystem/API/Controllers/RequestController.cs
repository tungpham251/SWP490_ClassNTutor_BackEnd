using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [Authorize(Roles = "STAFF")]
        [HttpGet("get-requests")]
        public async Task<IActionResult> GetRequests([FromQuery] RequestRequestDto entity)
        {
            var result = await _requestService.GetRequests(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpGet("get-request-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _requestService.GetById(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }


        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpPost("add-request")]
        public async Task<IActionResult> AddRequest([FromForm] AddRequestDto entity)
        {
            var result = await _requestService.AddRequest(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpGet("get-requests-of-tutor")]
        public async Task<IActionResult> GetRequestsOfTutor([FromQuery] RequestOfTutorRequestDto entity)
        {
            var result = await _requestService.GetRequestsOfTutor(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
