using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ClassNTutorContext _context;
        private readonly IMapper _mapper;

        public ScheduleService(ClassNTutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> GetScheduleForCurrentUserAndClassId(long personId, long classId)
        {
            var classes = _context.Classes.Include(c=>c.Schedules)
                                          .Where(c => c.TutorId.Equals(personId))
                                          .AsQueryable();

            var result = new List<ScheduleDto>();
            if(classId != 0)
            {
                var classByClassId = await classes.FirstOrDefaultAsync(c => c.ClassId.Equals(classId)).ConfigureAwait(false);
                if (classByClassId == null)
                    return Enumerable.Empty<ScheduleDto>();
                result = _mapper.Map<List<ScheduleDto>>(classByClassId.Schedules);
            }
            else
            {
                result = _mapper.Map<List<ScheduleDto>>(classes.SelectMany(c => c.Schedules));
            }
            return  result;
        }
    }
}
