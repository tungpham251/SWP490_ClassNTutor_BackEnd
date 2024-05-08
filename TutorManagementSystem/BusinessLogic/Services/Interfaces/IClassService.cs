using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IClassService
    {
        Task<ViewPaging<ClassDto>> GetClassesForTutor(ClassForTutorRequestDto entity);

        Task<ViewPaging<ClassDto>> GetClassesForParent(ClassForParentRequestDto entity);
        Task<ViewPaging<ClassDto>> GetClasses(ClassRequestDto entity);
        Task<ViewPaging<StudentDto>> GetStudentsInClass(StudentInClassRequestDto entity);
        Task<bool> AddStudentsInClass(List<AddStudentInClassRequestDto> entity);
        Task<bool> DeleteStudentInClass(DeleteStudentInClassRequestDto entity);
        Task<ClassDto> GetById(long id);

        Task<bool> AddClass(AddClassDto entity);

        Task<bool> UpdateClass(UpdateClassDto entity);

        Task<bool> AddClassIncludeSchedule(AddClassIncludeScheduleDto entity);
        Task<bool> UpdateClassIncludeSchedule(UpdateClassIncludeScheduleDto entity);

        Task<bool> DeleteClassById(long personId, long classId);
        Task<ClassDetailsIncludeStudentInfoDto> GetClassByIdIncludeStudentInformation(long id);
        Task<ClassDetailsIncludeScheduleStudentDto> GetClassByIdIncludeScheduleStudentInformation(long id, long parentId);

    }
}
