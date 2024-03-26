using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class RequestService : IRequestService
    {
        private readonly ClassNTutorContext _context;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public RequestService(ClassNTutorContext context, IRequestRepository requestRepository, IMapper mapper)
        {
            _context = context;
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<RequestDto> GetById(long id)
        {
            var result = await _requestRepository.GetRequestDetail(id).FirstOrDefaultAsync().ConfigureAwait(false);

            if (result == null) return null;

            return result;
        }

        public async Task<ViewPaging<RequestDto>> GetRequests(RequestRequestDto entity)
        {
            var search = _requestRepository.SearchRequest(entity.SubjectId);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.RequestId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<RequestDto>>(pagingList);


            return new ViewPaging<RequestDto>(result, pagination);
        }
    }
}
