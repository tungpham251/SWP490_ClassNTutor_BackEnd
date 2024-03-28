using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ClassService : IClassService
    {
        private readonly ClassNTutorContext _context;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassService(ClassNTutorContext context, IClassRepository classRepository, IMapper mapper)
        {
            _context = context;
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddClass(AddClassDto entity)
        {
            try
            {
                var lastClassId = await _context.Classes.OrderBy(x => x.ClassId).LastOrDefaultAsync().ConfigureAwait(false);

                var newClass = _mapper.Map<Class>(entity);
                newClass.ClassId = lastClassId.ClassId+1;
                newClass.CreatedAt = newClass.UpdatedAt = DateTime.Now;

                await _context.Classes.AddAsync(newClass).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ClassDto> GetById(long id)
        {
            var result = await _classRepository.GetClassDetail(id).FirstOrDefaultAsync().ConfigureAwait(false);

            if (result == null) return null;

            return result;
        }

        public async Task<ViewPaging<ClassDto>> GetClasses(ClassRequestDto entity)
        {
            var search = _classRepository.SearchClass(entity.SearchWord, entity.Status);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.ClassId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<ClassDto>>(pagingList);


            return new ViewPaging<ClassDto>(result, pagination);
        }
    }
}
