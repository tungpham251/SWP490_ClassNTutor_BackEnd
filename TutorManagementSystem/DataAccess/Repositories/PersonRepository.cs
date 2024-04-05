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


    }
}
