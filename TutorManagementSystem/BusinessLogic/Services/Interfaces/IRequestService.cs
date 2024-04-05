using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRequestService
    {
        Task<ViewPaging<RequestDto>> GetRequests(RequestRequestDto entity);

        Task<ViewPaging<RequestDto>> GetRequestsOfTutor(RequestOfTutorRequestDto entity);

        Task<RequestDto> GetById(long id);

        Task<bool> AddRequest(AddRequestDto entity);
    }
}
