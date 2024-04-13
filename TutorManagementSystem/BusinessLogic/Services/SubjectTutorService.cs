using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class SubjectTutorService : ISubjectTutorService
    {
        private readonly ClassNTutorContext _context;
        private readonly IMapper _mapper;

        public SubjectTutorService(ClassNTutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddSubjectTutor(AddSubjectTutorDto entity)
        {
            try
            {
                var newSubjectTutor = _mapper.Map<SubjectTutor>(entity);
                await _context.SubjectTutors.AddAsync(newSubjectTutor).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSubjectTutor(long tutorId, long subjectId)
        {
            try
            {
                var oldSubjectTutor = await _context.SubjectTutors.FirstOrDefaultAsync(s => s.TutorId == tutorId && s.SubjectId == subjectId)
                                                       .ConfigureAwait(false);
                if (oldSubjectTutor == null)
                    return false;
                _context.SubjectTutors.Remove(oldSubjectTutor);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
