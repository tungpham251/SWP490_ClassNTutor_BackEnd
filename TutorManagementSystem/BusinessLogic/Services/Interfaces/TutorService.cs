using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services
{
    public class TutorService : ITutorService
    {
        private readonly ClassNTutorContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IS3StorageService _s3storageService;
        private readonly IMapper _mapper;

        public TutorService(ClassNTutorContext context, IConfiguration config,
            IEmailService emailService, IS3StorageService s3storageService, IMapper mapper)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
            _s3storageService = s3storageService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTutorDto>> GetAllTutorActive(string subjectName)
        {

            if (string.IsNullOrEmpty(subjectName))
                subjectName = "";
            subjectName = subjectName.Trim();
            var tutors =await _context.SubjectTutors
                .Include(st => st.Subject)
                .Include(st => st.Tutor)
                .ThenInclude(t => t.Person)
                .ThenInclude(p => p.Account)
                .Where(st => st.Tutor.Person.Account.Status.Equals("ACTIVE") && 
                       st.Subject.SubjectName.ToLower().Contains(subjectName.ToLower()))
                .Select(st=>st.Tutor).ToListAsync().ConfigureAwait(false);

            var result = _mapper.Map<IEnumerable<GetTutorDto>>(tutors);
            return result;  
        }
    }
}
