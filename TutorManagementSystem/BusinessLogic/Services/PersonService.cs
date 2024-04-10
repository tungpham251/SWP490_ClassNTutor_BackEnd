using AutoMapper;
using BusinessLogic.Helpers;
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
        private const int TUTOR = 1;
        private const int PARENT = 2;

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

        public async Task<ProfileDto> GetProfileByCurrentUser(string personId)
        {
            var currentPersonId = long.Parse(personId);
            var profile = await _context.People.Include(p => p.Account)
                           .ThenInclude(a => a.Role)
                           .FirstOrDefaultAsync(p => p.PersonId.Equals(currentPersonId))
                           .ConfigureAwait(false);
            var result = _mapper.Map<ProfileDto>(profile);
            result.Students = Enumerable.Empty<StudentDto>();
            result.SubjectTutors = Enumerable.Empty<SubjectTutorDto>();

            if (profile == null)
                return result;
            if (profile.Account != null)
            {
                if (profile.Account.RoleId.Equals(TUTOR))
                {
                    result.SubjectTutors = _mapper.Map<IEnumerable<SubjectTutorDto>>(await _context.SubjectTutors
                                                                        .Include(st => st.Subject)
                                                                        .Where(st => st.TutorId.Equals(currentPersonId))
                                                                        .ToListAsync().ConfigureAwait(false));
                }
                if (profile.Account.RoleId.Equals(PARENT))
                {
                    result.Students = _mapper.Map<IEnumerable<StudentDto>>(await _context.Students
                                                                        .Include(s => s.Parent)
                                                                        .Include(s => s.StudentNavigation)
                                                                        .Where(st => st.ParentId.Equals(currentPersonId))
                                                                        .ToListAsync().ConfigureAwait(false));
                }
            }
            return result;

        }

        public async Task<bool> EditProfileCurrentUser(EditProfileDto entity, string personId)
        {
            try
            {
                if (!FileHelper.IsImage(entity.Avatar.FileName))
                {
                    return false;
                }
                var currentPersonId = long.Parse(personId);
                var currentUser = await _context.People.FirstOrDefaultAsync(p => p.PersonId.Equals(currentPersonId)).ConfigureAwait(false);
                var avatar = await _s3storageService.UploadFileToS3(entity.Avatar!).ConfigureAwait(false);

                var lastPerson = await _context.People.OrderBy(x => x.PersonId).LastOrDefaultAsync().ConfigureAwait(false);

                currentUser.UserAvatar = avatar;
                currentUser.Address = entity.Address!;
                currentUser.Dob = entity.Dob;
                currentUser.Gender = entity.Gender!;
                currentUser.FullName = entity.FullName!;
                currentUser.Phone = entity.Phone!;

                _context.People.Update(currentUser);
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
