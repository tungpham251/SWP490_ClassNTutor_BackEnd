using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly ClassNTutorContext _context;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(ClassNTutorContext context, IRoleRepository roleRepository, IMapper mapper)
        {
            _context = context;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddRole(RoleDto entity)
        {
            try
            {
                var checkDuplicate = await _context.Roles
                .Where(x => x.RoleName.Trim().ToLower()
                .Equals(entity.RoleName!.Trim().ToLower()))
                .FirstOrDefaultAsync().ConfigureAwait(false);

                if (checkDuplicate != null)
                {
                    return false;
                }

                var lastRoleId = await _context.Roles.OrderBy(x => x.RoleId).LastOrDefaultAsync().ConfigureAwait(false);

                var newRole = _mapper.Map<Role>(entity);
                newRole.RoleId = lastRoleId!.RoleId + 1;
                newRole.CreatedAt = newRole.UpdatedAt = DateTime.Now;
                await _context.Roles.AddAsync(newRole).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRole(int id)
        {
            try
            {
                var role = await _context.Roles
                .Where(x => x.RoleId == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

                if (role == null) return false;

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<RoleDto> GetById(int id)
        {
            var role = await _context.Roles
              .Where(x => x.RoleId == id)
              .FirstOrDefaultAsync()
              .ConfigureAwait(false);

            if (role == null) return null;

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<ViewPaging<RoleDto>> GetRoles(RoleRequestDto entity)
        {
            var search = _roleRepository.SearchRoles(entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.RoleId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<RoleDto>>(pagingList);


            return new ViewPaging<RoleDto>(result, pagination);
        }

        public async Task<bool> UpdateRole(RoleDto entity)
        {
            try
            {
                var role = await _context.Roles
               .Where(x => x.RoleId == entity.RoleId)
               .FirstOrDefaultAsync()
               .ConfigureAwait(false);

                if (role == null) return false;

                role.RoleName = entity.RoleName!;
                role.Status = entity.Status!;
                role.CreatedAt = role.UpdatedAt = DateTime.Now;
                _context.Roles.Update(role);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
