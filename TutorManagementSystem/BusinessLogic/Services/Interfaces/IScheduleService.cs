using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<IEnumerable<ScheduleDto>> GetScheduleForCurrentUserAndClassId(long personId, long classId);
        public Task<IEnumerable<FilterScheduleDto>> FilterScheduleFromTo(TimeSpan from, TimeSpan to);
    }
}
