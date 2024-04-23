using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<ScheduleDto>> GetScheduleForCurrentUserAndClassId(long personId, long classId);
        public Task<IEnumerable<FilterScheduleDto>> FilterScheduleFromTo(DateTime? from, DateTime? to, long classId, long personId, string studentName);
    }
}
