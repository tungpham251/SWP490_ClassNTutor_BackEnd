using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAttendentService
    {
        Task<IEnumerable<AttendentDto>> GetStudentsAttend(long scheduleId);

        Task<bool> UpdateStudentsAttend(List<AttendentRequestDto> entity);
    }
}
