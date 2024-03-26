using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ClassNTutorContext _context;

        public RoleRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<Role> SearchRole(string searchWord, string status)
        {
            var result = _context.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
                result = result.Where(x => x.RoleName.ToLower()
                .Contains(searchWord.ToLower()));

            if (status != null)
            {
                result = result.Where(c => c.Status.Equals(status));
            }

            return result.OrderBy(x => x.RoleId);
        }
    }
}
