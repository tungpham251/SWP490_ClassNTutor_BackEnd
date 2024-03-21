using SEP490_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace SEP490_BackEnd.DAL
{
    public static class UserDAO
    {
        public static Account? GetAccountByPersonId(int personId)
        {
            try
            {
                using (var context = new ClassNtutorContext())
                {
                    return context.Accounts
                        .Include(a => a.Person)
                        .ThenInclude(p => p.Tutor)
                        .FirstOrDefault(a => a.PersonId == personId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool UpdateAccountProfile(Account account)
        {
            try
            {
                using (var context = new ClassNtutorContext())
                {
                    context.Accounts.Update(account);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
