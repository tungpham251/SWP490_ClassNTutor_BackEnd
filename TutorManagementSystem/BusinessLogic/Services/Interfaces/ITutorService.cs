using DataAccess.Dtos;
using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITutorService
    {
        public Task<ViewPaging<GetTutorDto>> GetAllTutorActive(TutorRequestDto entity);
    }
}
