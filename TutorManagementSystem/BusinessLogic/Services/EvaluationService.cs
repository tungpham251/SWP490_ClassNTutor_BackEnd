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
                    .Where(x => x.Status.Trim().ToLower().Equals("CREATED".Trim().ToLower()))
                    .ToListAsync().ConfigureAwait(false);
                return _mapper.Map<List<EvaluationDto>>(evaluations);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
