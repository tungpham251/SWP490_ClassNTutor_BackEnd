using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface ISubjectTutorService
    {
        public Task<bool> AddScheduleTutor(AddSubjectTutorDto entity);
        public Task<bool> DeleteScheduleTutor(long id);
    }
}
