using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
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
        private readonly ITutorRepository _tutorRepository;

        public TutorService(ClassNTutorContext context, IConfiguration config,
            IEmailService emailService, IS3StorageService s3storageService, IMapper mapper, ITutorRepository tutorRepository)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
            _s3storageService = s3storageService;
            _mapper = mapper;
            _tutorRepository = tutorRepository;
        }

        public async Task<ViewPaging<GetTutorDto>> GetAllTutorActive(TutorRequestDto entity)
        {
            var search = _tutorRepository.SearchTutors(entity.subjectName!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.PersonId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<GetTutorDto>>(pagingList);

            return new ViewPaging<GetTutorDto>(result, pagination);
        }
    }
}
