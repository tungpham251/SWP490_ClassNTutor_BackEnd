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

        public IQueryable<FilterScheduleDto> FilterScheduleParent(DateTime? from, DateTime? to, long classId, long personId, string studentName)
        {
            var result = from c in _context.Classes
                         join s in _context.Schedules on c.ClassId equals s.ClassId
                         join a in _context.Attendents on s.Id equals a.ScheduleId
                         join st in _context.Students on a.StudentId equals st.StudentId
                         join p in _context.People on st.StudentId equals p.PersonId
                         where st.ParentId == personId
                         select new FilterScheduleDto
                         {
                             Id = s.Id,
                             DayOfWeek = s.DayOfWeek,
                             SessionEnd = s.SessionEnd,
                             SessionStart = s.SessionStart,
                             Date = s.Date,
                             Status = s.Status,
                             ClassId = c.ClassId,
                             ClassName = c.ClassName,
                             StudentName = p.FullName,
                             Attendent = a.Attentdent
                         };

            if (!from.Equals(null))
            {
                result = result.Where(x => x.Date >= from);
            }
            if (!to.Equals(null))
            {
                result = result.Where(x => x.Date <= to);
            }
            if (classId != 0)
            {
                result = result.Where(x => x.ClassId == classId);
            }
            if (!String.IsNullOrEmpty(studentName))
            {
                result = result.Where(x => x.StudentName == studentName);
            }

            return result;
        }

        public IQueryable<FilterScheduleDto> FilterScheduleTutor(DateTime? from, DateTime? to, long classId, long personId)
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
                             Date = s.Date,
                             Status = s.Status,
                             ClassId = c.ClassId,
                             ClassName = c.ClassName
                         };
            if (!from.Equals(null))
            {
                result = result.Where(x => x.Date >= from);
            }
            if (!to.Equals(null))
            {
                result = result.Where(x => x.Date <= to);
            }
            if (classId != 0)
            {
                result = result.Where(x => x.ClassId == classId);
            }

            return result;
        }


    }
}
