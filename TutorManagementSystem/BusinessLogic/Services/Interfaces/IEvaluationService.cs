

using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEvaluationService
    {
        Task<ViewPaging<EvaluationDto>> GetAllEvaluationByClassId(RequestEvaluationDto entity);

        Task<EvaluationDto> GetDetailEvaluation(int id);

        Task<bool> AddEvaluation(AddEvaluationDto entity);

        Task<ViewPaging<EvaluationDto>> GetEvaluationForParent(EvaluationForParentDto entity);
    }
}
