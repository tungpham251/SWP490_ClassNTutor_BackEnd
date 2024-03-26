using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRequestService
    {
        Task<ViewPaging<RequestDto>> GetRequests(RequestRequestDto entity);

        Task<RequestDto> GetById(long id);
    }
}
