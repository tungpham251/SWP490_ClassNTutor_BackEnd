using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        IQueryable<FilterScheduleDto> FilterSchedule(TimeSpan from, TimeSpan to);
    }

}