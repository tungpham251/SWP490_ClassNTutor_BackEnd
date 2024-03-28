using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<Role> SearchRole(string searchWord, string status);
    }
}
