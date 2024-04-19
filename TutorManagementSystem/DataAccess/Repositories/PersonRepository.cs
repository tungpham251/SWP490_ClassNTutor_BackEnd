using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ClassNTutorContext _context;

        public PersonRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<PersonDto> SearchAccounts(string searchWord, string status)
        {
            var query = from p in _context.People
                        join a in _context.Accounts on p.PersonId equals a.PersonId
                        join r in _context.Roles on a.RoleId equals r.RoleId
                        where r.RoleId != 0 && r.RoleId != 3
                        select new PersonDto
                        {
                            PersonId = p.PersonId,
                            FullName = p.FullName,
                            UserAvatar = p.UserAvatar,
                            Phone = p.Phone,
                            Gender = p.Gender,
                            Address = p.Address,
                            Dob = p.Dob,
                            Email = a.Email,
                            RoleName = r.RoleName,
                            Status = a.Status
                        };

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(x => x.Email!.ToLower().Contains(searchWord.ToLower())
                || x.FullName!.ToLower().Contains(searchWord.ToLower())
                || x.Phone!.ToLower().Contains(searchWord.ToLower())
                || x.Gender!.ToLower().Contains(searchWord.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.PersonId);
        }

        public IQueryable<PersonDto> SearchStaffs(string searchWord, string status)
        {
            var query = from p in _context.People
                        join a in _context.Accounts on p.PersonId equals a.PersonId
                        join r in _context.Roles on a.RoleId equals r.RoleId
                        where r.RoleId == 0
                        select new PersonDto
                        {
                            PersonId = p.PersonId,
                            FullName = p.FullName,
                            UserAvatar = p.UserAvatar,
                            Phone = p.Phone,
                            Gender = p.Gender,
                            Address = p.Address,
                            Dob = p.Dob,
                            Email = a.Email,
                            RoleName = r.RoleName,
                            Status = a.Status
                        };

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(x => x.Email!.ToLower().Contains(searchWord.ToLower())
                || x.FullName!.ToLower().Contains(searchWord.ToLower())
                || x.Phone!.ToLower().Contains(searchWord.ToLower())
                || x.Gender!.ToLower().Contains(searchWord.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.PersonId);
        }
    }
}
