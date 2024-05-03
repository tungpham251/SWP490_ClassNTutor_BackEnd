using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpGet("search-filter-payment")]
        public async Task<IActionResult> SearchAndFilterPayment([FromQuery] SearchFilterPaymentDto entity)
        {
            var result = await _paymentService.SearchAndFilterPayment(entity).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            }
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpGet("get-payment-by-id/{paymentId}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] long paymentId)
        {
            var result = await _paymentService.GetPaymentById(paymentId).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            }
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpPut("update-payment/{paymentId}")]
        public async Task<IActionResult> UpdatePayment([FromRoute] long paymentId, [FromBody] string paymentDescription)
        {
            var result = await _paymentService.UpdatePayment(paymentId, paymentDescription).ConfigureAwait(false);
            if (result == true)
            {
                return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
            }
            return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpDelete("delete-payment/{paymentId}")]
        public async Task<IActionResult> DeletePayment([FromRoute] long paymentId)
        {
            var result = await _paymentService.DeletePayment(paymentId).ConfigureAwait(false);
            if (result == true)
            {
                return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
            }
            return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
        }


        [HttpPut("update-payment-description/{paymentId}")]
        public async Task<IActionResult> UpdatePaymentDescription([FromRoute] long paymentId, [FromBody] string paymentDescription)
        {
            var result = await _paymentService.UpdatePaymentDescription(paymentId, paymentDescription).ConfigureAwait(false);
            if (result == true)
            {
                return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
            }
            return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
        }

        [Authorize(Roles = "STAFF")]
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto entity)
        {
            var result = await _paymentService.CreatePayment(entity).ConfigureAwait(false);
            if (result == true)
            {
                return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
            }
            return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
        }

        [Authorize(Roles = "STAFF,TUTOR")]
        [HttpGet("get-payment-by-current-user")]
        public async Task<IActionResult> GetPaymentByCurrentUser([FromQuery] SearchFilterPaymentCurrentUserDto entity)
        {

            string personId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(personId))
                return BadRequest(new ApiFormatResponse(StatusCodes.Status404NotFound, false, "Login pls"));

            var result = await _paymentService.GetPaymentByCurrentUser(entity, personId).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            }
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }
    }
}
