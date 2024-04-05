using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IClassService
    {
        Task<ViewPaging<ClassDto>> GetClasses(ClassRequestDto entity);
        Task<ViewPaging<ClassDto>> GetClassesOfTutor(ClassOfTutorRequestDto entity);

        Task<ClassDto> GetById(long id);

        Task<bool> AddClass(AddClassDto entity);
    }
}
