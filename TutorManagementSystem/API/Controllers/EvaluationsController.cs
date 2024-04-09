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

        [HttpGet("get-all-evaluation-by-classId/{id}")]
        public async Task<IActionResult> GetAllEvaluationByClassId([FromRoute] int id)
        {
            var result = await _evaluationService.GetAllEvaluationByClassId(id).ConfigureAwait(false);
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
        public async Task<IActionResult> AddEvaluation([FromForm] EvaluationDto entity)
        {
            var result = await _evaluationService.AddEvaluation(entity).ConfigureAwait(false);
            if (!result) return BadRequest(new ApiFormatResponse(StatusCodes.Status400BadRequest, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        // DELETE: api/Evaluations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvaluation(long id)
        //{
        //    if (_context.Evaluations == null)
        //    {
        //        return NotFound();
        //    }
        //    var evaluation = await _context.Evaluations.FindAsync(id);
        //    if (evaluation == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Evaluations.Remove(evaluation);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EvaluationExists(long id)
        //{
        //    return (_context.Evaluations?.Any(e => e.EvaluationId == id)).GetValueOrDefault();
        //}
    }
}
