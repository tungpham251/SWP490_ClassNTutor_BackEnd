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

        public IQueryable<FilterScheduleDto> FilterScheduleParent(TimeSpan? from, TimeSpan? to, long classId, long personId)
        {
            var result = from s in _context.Schedules
                         join c in _context.Classes on s.ClassId equals c.ClassId
                         join m in _context.ClassMembers on c.ClassId equals m.ClassId
                         join t in _context.Students on m.StudentId equals t.StudentId
                         where t.ParentId == personId
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
            if (!from.Equals(null))
            {
                result = result.Where(x => x.SessionStart >= from);
            }
            if (!to.Equals(null))
            {
                result = result.Where(x => x.SessionEnd <= to);
            }
            if (!classId.Equals(null))
            {
                result = result.Where(x => x.ClassId == classId);
            }

            return result;
        }

        public IQueryable<FilterScheduleDto> FilterScheduleTutor(TimeSpan? from, TimeSpan? to, long classId, long personId)
        {
            var result = from s in _context.Schedules
                         join c in _context.Classes on s.ClassId equals c.ClassId
                         where c.TutorId == personId
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
            if (!from.Equals(null))
            {
                result = result.Where(x => x.SessionStart >= from);
            }
            if (!to.Equals(null))
            {
                result = result.Where(x => x.SessionEnd <= to);
            }
            if (classId != 0)
            {
                result = result.Where(x => x.ClassId == classId);
            }

            return result;
        }


    }
}
