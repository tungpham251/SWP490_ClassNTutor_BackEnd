using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        IQueryable<Subject> SearchSubject(string searchWord, string status);
    }
}
