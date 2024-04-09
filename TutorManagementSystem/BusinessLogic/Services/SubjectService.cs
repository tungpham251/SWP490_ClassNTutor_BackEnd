using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ClassNTutorContext _context;
        private readonly ISubjectRepository _SubjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ClassNTutorContext context, ISubjectRepository SubjectRepository, IMapper mapper)
        {
            _context = context;
            _SubjectRepository = SubjectRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddSubject(SubjectDto entity)
        {
            try
            {
                var checkDuplicate = await _context.Subjects
                .Where(x => x.SubjectName.Trim().ToLower()
                .Equals(entity.SubjectName!.Trim().ToLower()))
                .FirstOrDefaultAsync().ConfigureAwait(false);

                if (checkDuplicate != null)
                {
                    return false;
                }

                var lastSubjectId = await _context.Subjects.OrderBy(x => x.SubjectId).LastOrDefaultAsync().ConfigureAwait(false);

                var newSubject = _mapper.Map<Subject>(entity);
                newSubject.SubjectId = lastSubjectId!.SubjectId + 1;
                await _context.Subjects.AddAsync(newSubject).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSubject(int id)
        {
            try
            {
                var subject = await _context.Subjects
                .Where(x => x.SubjectId == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

                if (subject == null) return false;

                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<SubjectDto> GetById(int id)
        {
            var subject = await _context.Subjects
              .Where(x => x.SubjectId == id)
              .FirstOrDefaultAsync()
              .ConfigureAwait(false);

            if (subject == null) return null;

            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task<ViewPaging<SubjectDto>> GetSubjects(SubjectRequestDto entity)
        {
            var search = _SubjectRepository.SearchSubjects(entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.SubjectId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<SubjectDto>>(pagingList);


            return new ViewPaging<SubjectDto>(result, pagination);
        }

        public async Task<bool> UpdateSubject(SubjectDto entity)
        {
            try
            {
                var subject = await _context.Subjects
               .Where(x => x.SubjectId == entity.SubjectId)
               .FirstOrDefaultAsync()
               .ConfigureAwait(false);

                if (subject == null) return false;

                subject.SubjectName = entity.SubjectName!;
                subject.Status = entity.Status!;

                _context.Subjects.Update(subject);
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
