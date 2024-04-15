using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITutorRepository
    {
        IQueryable<GetTutorDto> SearchTutors(string subjectName);
    }

}