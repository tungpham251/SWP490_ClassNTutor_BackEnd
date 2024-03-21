using SEP490_BackEnd.DAL;
using SEP490_BackEnd.Models;
using SEP490_BackEnd.Repositories.Interface;

namespace SEP490_BackEnd.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository() { }
        public Account? GetAccountByPersonId(int personId)
        {
            return UserDAO.GetAccountByPersonId(personId);
        }

        public bool UpdateAccountProfile(Account account)
        {
            return UserDAO.UpdateAccountProfile(account);
        }
    }
}
