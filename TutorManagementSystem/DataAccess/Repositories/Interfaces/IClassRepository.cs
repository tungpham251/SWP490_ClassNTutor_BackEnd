using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IClassRepository
    {
        IQueryable<ClassDto> SearchClass(string searchWord, string status, long? subjectId);
        IQueryable<ClassDto> SearchClassForTutor(long tutorId, string searchWord, string status);
        IQueryable<ClassDto> SearchClassForParent(long parentId, string searchWord, string status);
        IQueryable<StudentDto> SearchStudentInParent(string searchWord);
        IQueryable<ClassDto> GetClassDetail(long id);

        void UpdateClass(Class entity);

        void DeleteClassById(long classId);

        Task<Class> GetClassByIdIncludeStudentInformation(long id);

        Task<Class> GetClassByIdIncludeScheduleStudentInformation(long id);
    }
}
