using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        IQueryable<FilterScheduleDto> FilterScheduleTutor(TimeSpan? from, TimeSpan? to, long classId, long personId);
        IQueryable<FilterScheduleDto> FilterScheduleParent(TimeSpan? from, TimeSpan? to, long classId, long personId);
    }

}