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
        private readonly IEvaluationRepository _evaluationRepository;

        public EvaluationService(ClassNTutorContext context, IMapper mapper, IEvaluationRepository evaluationRepository)
        {
            _context = context;
            _mapper = mapper;
            _evaluationRepository = evaluationRepository;
        }

        public async Task<ViewPaging<EvaluationDto>> GetAllEvaluationByClassId(RequestEvaluationDto entity)
        {

            var evaluations = _evaluationRepository.GetEvaluations(entity.ClassId, entity.StudentId, entity.StartDate, entity.EndDate);

            var pagingList = await evaluations.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
           .Take(entity.PagingRequest.PageSize).OrderBy(x => x.EvaluationId)
           .ToListAsync()
           .ConfigureAwait(false);

            var pagination = new Pagination(await evaluations.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<EvaluationDto>>(pagingList);


            return new ViewPaging<EvaluationDto>(result, pagination);


        }

        public async Task<EvaluationDto> GetDetailEvaluation(int id)
        {

            var result = await _evaluationRepository.GetDetailEvaluation(id).FirstOrDefaultAsync().ConfigureAwait(false);

            if (result == null) return null;

            return result;
        }

        public async Task<bool> AddEvaluation(AddEvaluationDto entity)
        {
            try
            {

                var lastEvaluationId = await _context.Evaluations.OrderBy(x => x.EvaluationId).LastOrDefaultAsync().ConfigureAwait(false);

                var newEvaluation = _mapper.Map<Evaluation>(entity);

                newEvaluation.EvaluationId = lastEvaluationId!.EvaluationId + 1;
                newEvaluation.Status = newEvaluation.Status = "CREATED";
                newEvaluation.UpdatedAt = newEvaluation.CreatedAt = DateTime.Now;
                await _context.Evaluations.AddAsync(newEvaluation).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ViewPaging<EvaluationDto>> GetEvaluationForParent(EvaluationForParentDto entity)
        {
            var evaluations = _evaluationRepository.GetEvaluationForParent(entity.ParentId, entity.StudentId, entity.StartDate, entity.EndDate);
            var pagingList = await evaluations.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                    .Take(entity.PagingRequest.PageSize).OrderBy(x => x.EvaluationId)
                    .ToListAsync()
                    .ConfigureAwait(false);

            var pagination = new Pagination(await evaluations.CountAsync(), entity.PagingRequest.CurrentPage,
            entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);
            var result = _mapper.Map<IEnumerable<EvaluationDto>>(pagingList);


            return new ViewPaging<EvaluationDto>(result, pagination);
        }

        public List<EvaluationDto> GetAllEvaluationByClassIdNoPaging(RequestEvaluationAllDto entity)
        {
            var evaluations = _evaluationRepository.GetEvaluations(entity.ClassId, entity.StudentId, entity.StartDate, entity.EndDate);

            var result = _mapper.Map<IEnumerable<EvaluationDto>>(evaluations);
            if (result == null) return null;
            return (List<EvaluationDto>)result;
        }

        public List<EvaluationDto> GetEvaluationForParentNoPaging(EvaluationForParentAllDto entity)
        {
            var evaluations = _evaluationRepository.GetEvaluationForParent(entity.ParentId, entity.StudentId, entity.StartDate, entity.EndDate);
            var result = _mapper.Map<IEnumerable<EvaluationDto>>(evaluations);
            if (result == null) return null;
            return (List<EvaluationDto>)result;
        }
    }
}
