using DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IEvaluationRepository
    {

        IQueryable<EvaluationDto> GetDetailEvaluation(long evaluationId);

        IQueryable<EvaluationDto> GetEvaluations(long classId, long? studentId, DateTime? startDate, DateTime? endDate);
        IQueryable<EvaluationDto> GetEvaluationForParent(long parentId, long? studentId, DateTime? startDate, DateTime? endDate);
    }
}
