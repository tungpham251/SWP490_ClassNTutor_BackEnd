using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class AttendentService : IAttendentService
    {
        private readonly ClassNTutorContext _context;
        private readonly IMapper _mapper;
        private const int TUTOR = 1;

        public AttendentService(ClassNTutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CheckScheduleClass(long tutorId)
        {
            try
            {
                var currentUser = await _context.People
                                                    .Include(p => p.Account)
                                                    .ThenInclude(a => a.Role)
                                                    .FirstOrDefaultAsync(p => p.PersonId.Equals(tutorId))
                                                    .ConfigureAwait(false);

                if (currentUser.Account.RoleId.Equals(TUTOR))
                {
                    DateTime currentDate = DateTime.Now;
                    var schedulesOfTutor = await _context.Schedules.Where(x => x.Class.TutorId == tutorId).ToListAsync().ConfigureAwait(false);
                    foreach (var schedule in schedulesOfTutor)
                    {                       

                        if (schedule.Date < currentDate.Date && schedule.SessionEnd.Hours < currentDate.Hour && schedule.SessionEnd.Minutes - currentDate.Minute > 2)
                        {
                            schedule.Status = "COMPLETED";
                        }
                    }
                    _context.Schedules.UpdateRange(schedulesOfTutor);

                    var lastSchedulesOfTutor = await _context.Schedules
                                                .Where(x => x.Class.TutorId == tutorId)
                                                .GroupBy(x => x.ClassId)
                                                .Select(g => g.OrderByDescending(x => x.Date).FirstOrDefault())
                                                .ToListAsync()
                                                .ConfigureAwait(false);
                    foreach (var schedule in lastSchedulesOfTutor)
                    {
                        if (schedule.Status.Equals("COMPLETED"))
                        {
                            var findClass = _context.Classes.Where(x => x.ClassId == schedule.ClassId).FirstOrDefault();
                            findClass.Status = "COMPLETED";
                            _context.Classes.Update(findClass);
                        }
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<IEnumerable<AttendentDto>> GetStudentsAttend(long scheduleId)
        {
            var students = await _context.Attendents.Where(x => x.ScheduleId == scheduleId).Include(x => x.Student).ThenInclude(x => x.StudentNavigation).ToListAsync().ConfigureAwait(false);
            if (students == null)
                return Enumerable.Empty<AttendentDto>();
            var result = _mapper.Map<List<AttendentDto>>(students);
            return result;
        }

        public async Task<bool> UpdateStudentsAttend(List<AttendentRequestDto> entity)
        {
            try
            {
                var schedule = _context.Schedules.Where(x => x.Id == entity.FirstOrDefault().ScheduleId).FirstOrDefault();
                if (schedule == null) return false;
                if (schedule.Status.Equals("COMPLETED")) return false;
                var attendents = _mapper.Map<List<Attendent>>(entity);
                if (attendents.Any())
                {
                    foreach (var attendent in attendents)
                    {
                        _context.Attendents.Update(attendent);
                    }
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
