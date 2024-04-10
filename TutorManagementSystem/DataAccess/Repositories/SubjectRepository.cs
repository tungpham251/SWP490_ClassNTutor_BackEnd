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

        public IQueryable<Subject> SearchSubjects(string searchWord, string status)
        {
            var result = _context.Subjects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
                result = result.Where(x => x.SubjectName.ToLower()
                .Contains(searchWord.ToLower()));

            if (!string.IsNullOrWhiteSpace(status))
            {
                result = result.Where(c => c.Status.Equals(status));
            }

            return result.OrderBy(x => x.SubjectId);
        }
    }
}
