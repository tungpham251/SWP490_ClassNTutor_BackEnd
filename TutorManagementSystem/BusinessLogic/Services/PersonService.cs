using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly ClassNTutorContext _context;
        private readonly IS3StorageService _s3storageService;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(ClassNTutorContext context,
            IS3StorageService s3storageService,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _context = context;
            _s3storageService = s3storageService;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<ViewPaging<PersonDto>> GetAccounts(PersonRequestDto entity)
        {
            var search = _personRepository.SearchAccounts(entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.PersonId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<PersonDto>>(pagingList);


            return new ViewPaging<PersonDto>(result, pagination);
        }

        public async Task<ViewPaging<PersonDto>> GetStaffs(PersonRequestDto entity)
        {
            var search = _personRepository.SearchStaffs(entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.PersonId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<PersonDto>>(pagingList);


            return new ViewPaging<PersonDto>(result, pagination);
        }
    }
}
