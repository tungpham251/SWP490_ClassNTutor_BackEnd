using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ClassNTutorContext _context;

        public SubjectRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<Subject> SearchSubject(string searchWord, string status)
        {
            var result = _context.Subjects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
                result = result.Where(x => x.SubjectName.ToLower()
                .Contains(searchWord.ToLower()));

            if (status != null)
            {
                result = result.Where(c => c.Status.Equals(status));
            }

            return result.OrderBy(x => x.SubjectId);
        }
    }
}
