using DataAccess.Dtos;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        IQueryable<RequestDto> SearchRequest(int subjectId);

        IQueryable<RequestDto> GetRequestDetail(long id);
    }
}
