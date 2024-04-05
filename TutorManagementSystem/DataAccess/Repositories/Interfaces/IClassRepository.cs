using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IClassRepository
    {
        IQueryable<ClassDto> SearchClass(string searchWord, string status);

        IQueryable<ClassDto> SearchClassOfTutor(int tutorId,string searchWord, string status);

        IQueryable<ClassDto> GetClassDetail(long id);
    }
}
