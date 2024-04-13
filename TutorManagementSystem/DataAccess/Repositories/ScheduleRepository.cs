using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ClassNTutorContext _context;

        public ScheduleRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<FilterScheduleDto> FilterSchedule(TimeSpan from, TimeSpan to)
        {
            var result = from s in _context.Schedules
                         join c in _context.Classes on s.ClassId equals c.ClassId
                         select new FilterScheduleDto
                         {
                             Id = s.Id,
                             DayOfWeek = s.DayOfWeek,
                             SessionEnd = s.SessionEnd,
                             SessionStart = s.SessionStart,
                             Status = s.Status,
                             ClassId = c.ClassId,
                             ClassName = c.ClassName
                         };
            if(!from.Equals(null))
            {
                result = result.Where(x => x.SessionStart >= from);
            }
            if (!to.Equals(null))
            {
                result = result.Where(x => x.SessionEnd <= to);
            }

            return result;
        }

        
    }
}
