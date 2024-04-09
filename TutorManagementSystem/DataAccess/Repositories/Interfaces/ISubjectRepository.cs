using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        IQueryable<Subject> SearchSubjects(string searchWord, string status);
    }
}
