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
        private const int STAFF = 0;
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

        public async Task<ProfileDto> GetProfileByPersonId(long personId)
        {
            var currentPersonId = personId;
            var profile = await _context.People
                           .Include(p => p.Account)
                           .ThenInclude(a => a.Role)
                           .Include(p => p.Staff)
                           .Include(p => p.Tutor)
                           .FirstOrDefaultAsync(p => p.PersonId.Equals(currentPersonId))
                           .ConfigureAwait(false);
            if (profile == null)
                return null;
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
                var avatar = "";
                if (entity.Avatar != null)
                {
                    avatar = await _s3storageService.UploadFileToS3(entity.Avatar!).ConfigureAwait(false);
                }

                var currentPersonId = long.Parse(personId);
                var currentUser = await _context.People
                                                    .Include(p => p.Account)
                                                    .ThenInclude(a => a.Role)
                                                    .FirstOrDefaultAsync(p => p.PersonId.Equals(currentPersonId))
                                                    .ConfigureAwait(false);

                var lastPerson = await _context.People.OrderBy(x => x.PersonId).LastOrDefaultAsync().ConfigureAwait(false);

                if (entity.Avatar != null)
                {
                    currentUser.UserAvatar = avatar;
                }
                if (entity.Address != null)
                {
                    currentUser.Address = entity.Address!;
                }
                if (entity.Dob != null)
                {
                    currentUser.Dob = entity.Dob!;
                }
                if (entity.Gender != null)
                {
                    currentUser.Gender = entity.Gender!;
                }
                if (entity.FullName != null)
                {
                    currentUser.FullName = entity.FullName!;
                }
                if (entity.Phone != null)
                {
                    currentUser.Phone = entity.Phone!;
                }

                _context.People.Update(currentUser);

                if (currentUser.Account.RoleId.Equals(STAFF))
                {
                    var staff = await _context.Staffs
                                                    .FirstOrDefaultAsync(p => p.PersonId.Equals(currentPersonId))
                                                    .ConfigureAwait(false);
                    staff.StaffType = entity.Staff.StaffType;
                    _context.Staffs.Update(staff);
                }

                if (currentUser.Account.RoleId.Equals(TUTOR))
                {
                    var tutor = await _context.Tutors
                                                     .FirstOrDefaultAsync(p => p.PersonId.Equals(currentPersonId))
                                                     .ConfigureAwait(false);
                    tutor.Cmnd = entity.Tutor.Cmnd;

                    var frontCmnd = "";
                    var backCmnd = "";
                    var cv = "";
                    if (entity.Tutor.FrontCmnd != null)
                    {
                        frontCmnd = await _s3storageService.UploadFileToS3(entity.Tutor.FrontCmnd!).ConfigureAwait(false);

                    }
                    if (entity.Tutor.BackCmnd != null)
                    {

                        backCmnd = await _s3storageService.UploadFileToS3(entity.Tutor.BackCmnd!).ConfigureAwait(false);
                    }
                    if (entity.Tutor.Cv != null)
                    {

                        cv = await _s3storageService.UploadFileToS3(entity.Tutor.Cv!).ConfigureAwait(false);
                    }

                    if (entity.Tutor.FrontCmnd != null)
                    {
                        tutor.FrontCmnd = frontCmnd;

                    }
                    if (entity.Tutor.BackCmnd != null)
                    {
                        tutor.BackCmnd = backCmnd;

                    }
                    if (entity.Tutor.Cv != null)
                    {
                        tutor.Cv = cv;
                    }

                    tutor.EducationLevel = entity.Tutor.EducationLevel;
                    tutor.School = entity.Tutor.School;
                    tutor.GraduationYear = entity.Tutor.GraduationYear;
                    tutor.About = entity.Tutor.About;
                    _context.Tutors.Update(tutor);

                }

                await _context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteStaff(long id)
        {
            try
            {
                var staff = await _context.Accounts.FirstOrDefaultAsync(p => p.PersonId.Equals(id))
                                                    .ConfigureAwait(false);
                if (staff == null)
                {
                    return false;
                }
                staff.Status = "SUSPEND";
                _context.Accounts.Update(staff);
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
