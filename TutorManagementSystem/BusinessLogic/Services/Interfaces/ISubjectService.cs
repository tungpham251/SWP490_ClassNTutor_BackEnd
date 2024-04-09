using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<ViewPaging<SubjectDto>> GetSubjects(SubjectRequestDto entity);

        Task<SubjectDto> GetById(int id);

        Task<bool> AddSubject(SubjectDto entity);

        Task<bool> UpdateSubject(SubjectDto entity);

        Task<bool> DeleteSubject(int id);
    }
}
