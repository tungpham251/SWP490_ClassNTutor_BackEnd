using DataAccess.Dtos;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        IQueryable<RequestDto> GetRequests(string status);

        IQueryable<RequestDto> SearchRequestsForParent(long parentId, string status, string requestType);

        IQueryable<RequestDto> SearchRequestsForTutor(long tutorId, string status, string requestType);

        IQueryable<RequestDto> GetRequestDetail(long id);
    }
}
