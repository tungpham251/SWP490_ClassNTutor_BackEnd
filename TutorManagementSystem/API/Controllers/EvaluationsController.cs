using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using BusinessLogic.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationsController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationsController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpGet("get-all-evaluation-by-classId")]
        public async Task<IActionResult> GetAllEvaluationByClassId([FromQuery] RequestEvaluationDto entity)
        {
            var result = await _evaluationService.GetAllEvaluationByClassId(entity).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }        

        [HttpGet("get-detail-evaluation/{id}")]
        public async Task<IActionResult> GetEvaluationByClassId([FromRoute] int id)
        {
            var result = await _evaluationService.GetDetailEvaluation(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        [HttpPost("add-evaluation")]
        public async Task<IActionResult> AddEvaluation([FromForm] AddEvaluationDto entity)
        {
            var result = await _evaluationService.AddEvaluation(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

       
    }
}
