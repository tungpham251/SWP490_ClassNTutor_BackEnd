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

        public AttendentService(ClassNTutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AttendentDto>> GetStudentsAttend(long scheduleId)
        {
            var students = await _context.Attendents.Where(x => x.ScheduleId == scheduleId).Include(x => x.Student).ThenInclude(x => x.Parent).ToListAsync().ConfigureAwait(false);
            if (students == null)
                return Enumerable.Empty<AttendentDto>();
            var result = _mapper.Map<List<AttendentDto>>(students);
            return result;
        }

        public async Task<bool> UpdateStudentsAttend(List<AttendentRequestDto> entity)
        {
            try
            {
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
