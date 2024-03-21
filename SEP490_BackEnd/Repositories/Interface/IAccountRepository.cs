using SEP490_BackEnd.Models;

namespace SEP490_BackEnd.Repositories.Interface
{
    public interface IAccountRepository
    {
        public Account? GetAccountByPersonId(int personId);
        bool UpdateAccountProfile(Account account);
    }
}
