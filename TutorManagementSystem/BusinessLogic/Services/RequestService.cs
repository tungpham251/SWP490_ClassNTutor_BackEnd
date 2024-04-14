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

        public async Task<UpdateRequestDto> AcceptRequest(long requestId)
        {
            try
            {
                var findRequest = await _context.Requests.Where(x => x.RequestId == requestId)
                    .FirstOrDefaultAsync().ConfigureAwait(false);

                if (findRequest != null 
                    && findRequest.Status.ToLower().Equals("JOIN".ToLower()))
                {
                    findRequest.RequestType = "ACCEPT";

                    _context.Requests.Update(findRequest);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    return _mapper.Map<UpdateRequestDto>(findRequest);
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddRequest(AddRequestDto entity)
        {
            try
            {
                var lastRequest = await _context.Requests.OrderBy(x => x.RequestId).LastOrDefaultAsync().ConfigureAwait(false);

                var newRequest = _mapper.Map<Request>(entity);

                newRequest.RequestId = lastRequest!.RequestId + 1;
                newRequest.UpdatedAt = newRequest.CreatedAt = DateTime.Now;

                await _context.Requests.AddAsync(newRequest).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<RequestDto> GetById(long id)
        {
            var result = await _requestRepository.GetRequestDetail(id).FirstOrDefaultAsync().ConfigureAwait(false);

            if (result == null) return null;

            return result;
        }

        public async Task<ViewPaging<RequestDto>> GetRequests(RequestRequestDto entity)
        {
            var search = _requestRepository.GetRequests(entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.RequestId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<RequestDto>>(pagingList);


            return new ViewPaging<RequestDto>(result, pagination);
        }

        public async Task<ViewPaging<RequestDto>> GetRequestsForParent(RequestRequestDto entity)
        {
            var search = _requestRepository.SearchRequestsForParent(entity.PersonId, entity.SubjectId, entity.Status);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.RequestId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<RequestDto>>(pagingList);


            return new ViewPaging<RequestDto>(result, pagination);
        }

        public async Task<ViewPaging<RequestDto>> GetRequestsForTutor(RequestRequestDto entity)
        {
            var search = _requestRepository.SearchRequestsForTutor(entity.PersonId, entity.SubjectId, entity.Status);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.RequestId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<RequestDto>>(pagingList);


            return new ViewPaging<RequestDto>(result, pagination);
        }

        public async Task<bool> UpdateRequest(UpdateRequestDto entity)
        {
            try
            {
                var findRequest = await _context.Requests.Where(x => x.RequestId == entity.RequestId)
                    .FirstOrDefaultAsync().ConfigureAwait(false);

                if (findRequest == null) return false;

                findRequest.ParentId = entity.ParentId;
                findRequest.TutorId = entity.TutorId;
                findRequest.StudentId = entity.StudentId;
                findRequest.RequestType = entity.RequestType!;
                findRequest.ClassId = entity.ClassId;
                findRequest.Level = entity.Level;
                findRequest.SubjectId = entity.SubjectId;
                findRequest.Price = entity.Price;
                findRequest.Status = entity.Status!;

                _context.Requests.Update(findRequest);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
