using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IClassService
    {
        Task<ViewPaging<ClassDto>> GetClasses(ClassRequestDto entity);

        Task<ClassDto> GetById(long id);

        Task<bool> AddClass(AddClassDto entity);

        Task<bool> UpdateClass(UpdateClassDto entity);
        Task<bool> DeleteClassById(long classId);
        Task<ClassDetailsIncludeStudentInfoDto> GetClassByIdIncludeStudentInformation(long id);
    }
}
