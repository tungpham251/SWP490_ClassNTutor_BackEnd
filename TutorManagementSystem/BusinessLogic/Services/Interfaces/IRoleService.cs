using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ViewPaging<RoleDto>> GetRoles(RoleRequestDto entity);

        Task<RoleDto> GetById(int id);

        Task<bool> AddRole(RoleDto entity);

        Task<bool> UpdateRole(RoleDto entity);

        Task<bool> DeleteRole(int id);
    }
}
