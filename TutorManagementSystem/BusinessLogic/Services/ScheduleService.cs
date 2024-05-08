using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ClassNTutorContext _context;
        private readonly IMapper _mapper;
        private readonly IScheduleRepository _scheduleRepository;
        private const int TUTOR = 1;
        private const int PARENT = 2;

        public ScheduleService(ClassNTutorContext context, IMapper mapper, IScheduleRepository scheduleRepository)
        {
            _context = context;
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<ScheduleDto>> GetScheduleForCurrentUserAndClassId(long personId, string classId)
        {
            var classes = _context.Classes.Include(c => c.Schedules)
                                          .Where(c => c.TutorId.Equals(personId))
                                          .AsQueryable();

            var result = new List<ScheduleDto>();
            if (!string.IsNullOrEmpty(classId))
            {
                var classByClassId = await classes.FirstOrDefaultAsync(c => c.ClassId.Equals(long.Parse(classId))).ConfigureAwait(false);
                if (classByClassId == null)
                    return Enumerable.Empty<ScheduleDto>();
                result = _mapper.Map<List<ScheduleDto>>(classByClassId.Schedules);
            }
            else
            {
                result = _mapper.Map<List<ScheduleDto>>(classes.SelectMany(c => c.Schedules));
            }
            return result;
        }

        public async Task<IEnumerable<FilterScheduleDto>> FilterScheduleFromTo(DateTime? from, DateTime? to, string classId, long personId, string studentName)
        {
            
            var currentUser = await _context.People
                                                    .Include(p => p.Account)
                                                    .ThenInclude(a => a.Role)
                                                    .FirstOrDefaultAsync(p => p.PersonId.Equals(personId))
                                                    .ConfigureAwait(false);

           
            IEnumerable<FilterScheduleDto> data = null;

            
                
                if (currentUser.Account.RoleId.Equals(TUTOR))
                {
                    data = await _scheduleRepository.FilterScheduleTutor(from, to, classId, personId).ToListAsync().ConfigureAwait(false);
                }
                if (currentUser.Account.RoleId.Equals(PARENT))
                {
                    data = await _scheduleRepository.FilterScheduleParent(from, to, classId, personId, studentName).ToListAsync().ConfigureAwait(false);
                }                
            
            var orderedData = data.OrderBy(s => GetDayOfWeekOrder(s.DayOfWeek)).ThenBy(s => s.SessionStart).ToList();
            return orderedData;

        }

        static int GetDayOfWeekOrder(string dayOfWeek)
        {
            switch (dayOfWeek.ToUpper())
            {
                case "MON":
                    return 1;
                case "TUE":
                    return 2;
                case "WED":
                    return 3;
                case "THU":
                    return 4;
                case "FRI":
                    return 5;
                case "SAT":
                    return 6;
                case "SUN":
                    return 7;
                default:
                    throw new ArgumentException("Invalid day of week: " + dayOfWeek);
            }
        }
    }
}
