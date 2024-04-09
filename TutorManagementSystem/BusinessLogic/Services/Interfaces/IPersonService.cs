using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ViewPaging<PersonDto>> GetStaffs(PersonRequestDto entity);

        Task<ViewPaging<PersonDto>> GetAccounts(PersonRequestDto entity);
    }
}
