

using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEvaluationService
    {
        Task<List<EvaluationDto>> GetAllEvaluationByClassId(int id);

        Task<EvaluationDto> GetDetailEvaluation(int id);

        Task<bool> AddEvaluation(EvaluationDto entity);
    }
}
