﻿using BusinessLogic.Services.Interfaces;
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

        [HttpGet("get-requests")]
        public async Task<IActionResult> GetRequests([FromQuery] RequestRequestDto entity)
        {
            var result = await _requestService.GetRequests(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpGet("get-requests-for-tutor")]
        public async Task<IActionResult> GetRequestsForTutor([FromQuery] RequestRequestDto entity)
        {
            var result = await _requestService.GetRequestsForTutor(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "PARENT")]
        [HttpGet("get-requests-for-parent")]
        public async Task<IActionResult> GetRequestsForParent([FromQuery] RequestRequestDto entity)
        {
            var result = await _requestService.GetRequestsForParent(entity).ConfigureAwait(false);
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR,PARENT")]
        [HttpGet("get-request-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _requestService.GetById(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }


        [Authorize(Roles = "TUTOR,PARENT")]
        [HttpPost("add-request")]
        public async Task<IActionResult> AddRequest([FromForm] AddRequestDto entity)
        {
            var result = await _requestService.AddRequest(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }


        [Authorize(Roles = "TUTOR,PARENT")]
        [HttpPut("update-request")]
        public async Task<IActionResult> UpdateRequest([FromForm] UpdateRequestDto entity)
        {
            var result = await _requestService.UpdateRequest(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR,PARENT")]
        [HttpPut("accept-request")]
        public async Task<IActionResult> AcceptRequest(long requestId)
        {
            var result = await _requestService.AcceptRequest(requestId).ConfigureAwait(false);
            if (result == null) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }


        [Authorize(Roles = "PARENT")]
        [HttpPut("cancel-request")]
        public async Task<IActionResult> CancelRequest(long requestId)
        {
            var result = await _requestService.CancelRequest(requestId).ConfigureAwait(false);
            if (result == null) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "TUTOR")]
        [HttpPut("decline-request")]
        public async Task<IActionResult> DeclineRequest(long requestId)
        {
            var result = await _requestService.DeclineRequest(requestId).ConfigureAwait(false);
            if (result == null) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
