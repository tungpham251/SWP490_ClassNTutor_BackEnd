using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly ClassNTutorContext _context;
        private readonly IMapper _mapper;

        public EvaluationService(ClassNTutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EvaluationDto>> GetAllEvaluationByClassId(int id)
        {
            try
            {
                var evaluations = await _context.Evaluations
                    .Where(x => x.ClassId == id)
                    .ToListAsync().ConfigureAwait(false);
                return _mapper.Map<List<EvaluationDto>>(evaluations);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EvaluationDto> GetDetailEvaluation(int id)
        {
            var evaluation = await _context.Evaluations
              .Where(x => x.EvaluationId == id)
              .FirstOrDefaultAsync()
              .ConfigureAwait(false);

            if (evaluation == null) return null;

            return _mapper.Map<EvaluationDto>(evaluation);
        }

        public async Task<bool> AddEvaluation(EvaluationDto entity)
        {
            try
            {
                var checkDuplicate = await _context.Evaluations
                .Where(x => x.EvaluationId == entity.EvaluationId)
                .FirstOrDefaultAsync().ConfigureAwait(false);

                if (checkDuplicate != null)
                {
                    return false;
                }

                var lastEvaluationId = await _context.Evaluations.OrderBy(x => x.EvaluationId).LastOrDefaultAsync().ConfigureAwait(false);

                var newEvaluation = _mapper.Map<Evaluation>(entity);
                newEvaluation.EvaluationId = lastEvaluationId.EvaluationId + 1;
                await _context.Evaluations.AddAsync(newEvaluation).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
