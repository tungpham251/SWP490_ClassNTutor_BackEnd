using DataAccess.Dtos;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        IQueryable<RequestDto> SearchRequestsForParent(long parentId, long subjectId);

        IQueryable<RequestDto> SearchRequestsForTutor(long tutorId, long subjectId);

        IQueryable<RequestDto> GetRequestDetail(long id);
    }
}
