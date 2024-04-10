using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IStudentService
    {
        public Task<bool> AddStudent(AddStudentDto entity);
        public Task<bool> UpdateStudent(UpdateStudentDto entity);
        public Task<bool> DeleteStudent(long id);
    }
}
