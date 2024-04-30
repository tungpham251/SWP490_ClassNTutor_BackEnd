

using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEvaluationService
    {
        Task<ViewPaging<EvaluationDto>> GetAllEvaluationByClassId(RequestEvaluationDto entity);

        Task<EvaluationDto> GetDetailEvaluation(int id);

        Task<bool> AddEvaluation(AddEvaluationDto entity);

        Task<ViewPaging<EvaluationDto>> GetEvaluationForParent(EvaluationForParentDto entity);

        List<EvaluationDto> GetAllEvaluationByClassIdNoPaging(RequestEvaluationAllDto entity);

        List<EvaluationDto> GetEvaluationForParentNoPaging(EvaluationForParentAllDto entity);
    }
}
