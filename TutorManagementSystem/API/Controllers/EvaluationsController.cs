using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;

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

        [HttpGet("get-evaluation-by-id/{id}")]
        public async Task<IActionResult> GetAllEvaluationByClassId([FromRoute] int id)
        {
            var result = await _evaluationService.GetAllEvaluationByClassId(id).ConfigureAwait(false);
            if (result == null) return NotFound(new ApiFormatResponse(StatusCodes.Status404NotFound, false, result));
            return Ok(new ApiFormatResponse(StatusCodes.Status200OK, true, result));
        }

        // GET: api/Evaluations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Evaluation>> GetEvaluation(long id)
        //{
        //  if (_context.Evaluations == null)
        //  {
        //      return NotFound();
        //  }
        //    var evaluation = await _context.Evaluations.FindAsync(id);

        //    if (evaluation == null)
        //    {
        //        return NotFound();
        //    }

        //    return evaluation;
        //}

        // PUT: api/Evaluations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEvaluation(long id, Evaluation evaluation)
        //{
        //    if (id != evaluation.EvaluationId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(evaluation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EvaluationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Evaluations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        //{
        //  if (_context.Evaluations == null)
        //  {
        //      return Problem("Entity set 'ClassNTutorContext.Evaluations'  is null.");
        //  }
        //    _context.Evaluations.Add(evaluation);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (EvaluationExists(evaluation.EvaluationId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetEvaluation", new { id = evaluation.EvaluationId }, evaluation);
        //}

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
