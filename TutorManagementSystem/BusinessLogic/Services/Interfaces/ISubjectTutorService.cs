using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface ISubjectTutorService
    {
        public Task<bool> AddSubjectTutor(AddSubjectTutorDto entity);
        public Task<bool> DeleteSubjectTutor(long tutorId, long subjectId, int level);
    }
}
