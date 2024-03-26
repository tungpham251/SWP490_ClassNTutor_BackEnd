using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
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

        [HttpGet("get-requests")]
        public async Task<IActionResult> GetRequests([FromQuery] RequestRequestDto entity)
        {
            var result = await _requestService.GetRequests(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpGet("get-request-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _requestService.GetById(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
