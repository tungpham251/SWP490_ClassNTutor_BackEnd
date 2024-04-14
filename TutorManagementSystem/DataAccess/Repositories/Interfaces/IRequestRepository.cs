using DataAccess.Dtos;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        IQueryable<RequestDto> GetRequests(string status);

        IQueryable<RequestDto> SearchRequestsForParent(long parentId, long subjectId, string status, string requestType);

        IQueryable<RequestDto> SearchRequestsForTutor(long tutorId, long subjectId, string status, string requestType);

        IQueryable<RequestDto> GetRequestDetail(long id);
    }
}
