using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<Role> SearchRoles(string searchWord, string status);
    }
}
