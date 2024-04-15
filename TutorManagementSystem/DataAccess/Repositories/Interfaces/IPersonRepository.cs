using DataAccess.Dtos;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        IQueryable<PersonDto> SearchStaffs(string searchWord, string status);

        IQueryable<PersonDto> SearchAccounts(string searchWord, string status);
    }
}
