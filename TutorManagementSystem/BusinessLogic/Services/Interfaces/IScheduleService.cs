using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<ScheduleDto>> GetScheduleForCurrentUserAndClassId(long personId, string classId);
        public Task<IEnumerable<FilterScheduleDto>> FilterScheduleFromTo(DateTime? from, DateTime? to, string classId, long personId, string studentName);
    }
}
