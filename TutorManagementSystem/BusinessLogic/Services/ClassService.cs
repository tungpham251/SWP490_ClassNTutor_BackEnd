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
                newClass.ClassId = lastClassId.ClassId + 1;
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
            var search = _classRepository.SearchClass(entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.ClassId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<ClassDto>>(pagingList);


            return new ViewPaging<ClassDto>(result, pagination);
        }

        public async Task<ViewPaging<ClassDto>> GetClassesForTutor(ClassForTutorRequestDto entity)
        {
            var search = _classRepository.SearchClassForTutor(entity.TutorId, entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.ClassId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<ClassDto>>(pagingList);

            return new ViewPaging<ClassDto>(result, pagination);
        }

        public async Task<ViewPaging<ClassDto>> GetClassesForParent(ClassForParentRequestDto entity)
        {
            var search = _classRepository.SearchClassForParent(entity.ParentId, entity.SearchWord!, entity.Status!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.ClassId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<ClassDto>>(pagingList);

            return new ViewPaging<ClassDto>(result, pagination);
        }

        public async Task<bool> UpdateClass(UpdateClassDto entity)
        {
            try
            {
                var result = await _context.Classes.FirstOrDefaultAsync(c => c.ClassId.Equals(entity.ClassId)).ConfigureAwait(false);
                if (result == null)
                    return false;

                result.TutorId = entity.TutorId;
                result.ClassName = entity.ClassName;
                result.ClassDesc = entity.ClassDesc;
                result.ClassLevel = entity.ClassLevel;
                result.Price = entity.Price;
                result.SubjectId = entity.SubjectId;
                result.StartDate = entity.StartDate;
                result.EndDate = entity.EndDate;
                result.MaxCapacity = entity.MaxCapacity;

                _classRepository.UpdateClass(result);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteClassById(long classId)
        {
            try
            {

                var listSchedule = _context.Classes.Include(c => c.Schedules)
                                                   .FirstOrDefault(c => c.ClassId.Equals(classId)).Schedules;
                if (listSchedule.Any())
                {
                    listSchedule.ToList().ForEach(s => s.Status = "SUSPEND");
                    _context.Schedules.UpdateRange(listSchedule);
                }

                _classRepository.DeleteClassById(classId);

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ClassDetailsIncludeStudentInfoDto> GetClassByIdIncludeStudentInformation(long id)
        {
            var classDetails = await _classRepository.GetClassByIdIncludeStudentInformation(id).ConfigureAwait(false);
            if (classDetails == null)
            {
                return null;
            }
            var result = _mapper.Map<ClassDetailsIncludeStudentInfoDto>(classDetails);
            if (classDetails.ClassMembers != null)
                result.StudentInformationDto = _mapper.Map<IEnumerable<StudentInformationDto>>(classDetails.ClassMembers);

            if (classDetails.Schedules.Any())
            {
                result.Schedules = _mapper.Map<IEnumerable<ScheduleDto>>(classDetails.Schedules);
            }

            return result;
        }

        public async Task<ViewPaging<StudentDto>> GetStudentsInClass(StudentInClassRequestDto entity)
        {
            var search = _classRepository.SearchStudentInParent(entity.SearchWord!);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.StudentId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<StudentDto>>(pagingList);


            return new ViewPaging<StudentDto>(result, pagination);
        }

        public async Task<bool> AddStudentsInClass(List<AddStudentInClassRequestDto> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    var findLast = await _context.ClassMembers.OrderBy(x => x.Id).LastOrDefaultAsync().ConfigureAwait(false);
                    var classMember = _mapper.Map<ClassMember>(item);
                    classMember.Id = findLast!.Id + 1;
                    classMember.Status = "CREATED";
                    await _context.ClassMembers.AddAsync(classMember).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudentsInClass(List<long> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    var classMember = await _context.ClassMembers.Where(x=> x.Id == item)
                        .FirstOrDefaultAsync().ConfigureAwait(false);
                    if(classMember == null) return false;
                   
                    classMember.Status = "DELETED";
                    _context.ClassMembers.Update(classMember);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddClassIncludeSchedule(AddClassIncludeScheduleDto entity)
        {
            try
            {
                var lastClassId = await _context.Classes.OrderBy(x => x.ClassId).LastOrDefaultAsync().ConfigureAwait(false);
                var newClass = _mapper.Map<Class>(entity);
                newClass.ClassId = lastClassId.ClassId + 1;
                newClass.CreatedAt = newClass.UpdatedAt = DateTime.Now;
                await _context.Classes.AddAsync(newClass).ConfigureAwait(false);

                //add add schedule
                if (entity.AddScheduleDto != null)
                {
                    var lastSchedule = await _context.Schedules.OrderBy(x => x.Id).LastOrDefaultAsync().ConfigureAwait(false);
                    var newScheduleId = lastSchedule.Id + 1;
                    var newSchedules = _mapper.Map<IEnumerable<Schedule>>(entity.AddScheduleDto);
                    newSchedules.ToList().ForEach(s => { s.ClassId = newClass.ClassId; s.Id = newScheduleId++; });
                    await _context.Schedules.AddRangeAsync(newSchedules).ConfigureAwait(false);
                }
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateClassIncludeSchedule(UpdateClassIncludeScheduleDto entity)
        {
            try
            {
                var result = await _context.Classes.FirstOrDefaultAsync(c => c.ClassId.Equals(entity.ClassId)).ConfigureAwait(false);
                if (result == null)
                    return false;

                result.TutorId = entity.TutorId;
                result.ClassName = entity.ClassName;
                result.ClassDesc = entity.ClassDesc;
                result.ClassLevel = entity.ClassLevel;
                result.Price = entity.Price;
                result.SubjectId = entity.SubjectId;
                result.StartDate = entity.StartDate;
                result.EndDate = entity.EndDate;
                result.MaxCapacity = entity.MaxCapacity;

                _classRepository.UpdateClass(result);
                //update schedule
                if (entity.UpdateScheduleDto != null)
                {
                    var addSchedules = entity.UpdateScheduleDto.Where(s => s.Id.Equals(0L));
                    var updateSchedule = entity.UpdateScheduleDto.Where(s => !s.Id.Equals(0L));
                    //add new schedule
                    if (addSchedules.Any())
                    {
                        var lastSchedule = await _context.Schedules.OrderBy(x => x.Id).LastOrDefaultAsync().ConfigureAwait(false);
                        var newScheduleId = lastSchedule.Id + 1;
                        var newSchedules = _mapper.Map<IEnumerable<Schedule>>(addSchedules);
                        newSchedules.ToList().ForEach(s => { s.ClassId = result.ClassId; s.Id = newScheduleId++; });
                        await _context.Schedules.AddRangeAsync(newSchedules).ConfigureAwait(false);
                    }
                    //update old schedule
                    if (updateSchedule.Any())
                    {
                        var updateSchedules = new List<Schedule>();
                        foreach (var schedule in updateSchedule)
                        {
                            var oldSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id.Equals(schedule.Id)).ConfigureAwait(false);
                            oldSchedule.DayOfWeek = schedule.DayOfWeek;
                            oldSchedule.SessionStart = schedule.SessionStart;
                            oldSchedule.SessionEnd = schedule.SessionEnd;
                            oldSchedule.Status = schedule.Status;
                            updateSchedules.Add(oldSchedule);
                        }
                        _context.Schedules.UpdateRange(updateSchedules);
                    }

                }
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
