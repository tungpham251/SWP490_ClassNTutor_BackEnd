using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRequestService
    {
        Task<ViewPaging<RequestDto>> GetRequestsForTutor(RequestRequestDto entity);

        Task<ViewPaging<RequestDto>> GetRequestsForParent(RequestRequestDto entity);

        Task<RequestDto> GetById(long id);

        Task<bool> AddRequest(AddRequestDto entity);

        Task<bool> UpdateRequest(UpdateRequestDto entity);
    }
}
