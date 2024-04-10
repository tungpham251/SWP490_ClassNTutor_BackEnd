using DataAccess.Dtos;


namespace BusinessLogic.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ViewPaging<PersonDto>> GetStaffs(PersonRequestDto entity);

        Task<ViewPaging<PersonDto>> GetAccounts(PersonRequestDto entity);

        public Task<ProfileDto> GetProfileByCurrentUser(string personId);
        public Task<bool> EditProfileCurrentUser(EditProfileDto entity, string personId);

    }
}
