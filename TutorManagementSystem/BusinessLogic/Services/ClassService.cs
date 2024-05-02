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
            var search = _classRepository.SearchClass(entity.SearchWord!, entity.Status!, entity.SubjectId!);

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
                    listSchedule.ToList().ForEach(s => s.Status = "DELETED");
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

        public async Task<ClassDetailsIncludeScheduleStudentDto> GetClassByIdIncludeScheduleStudentInformation(long id, long parentId)
        {
            var classDetails = await _classRepository.GetClassByIdIncludeScheduleStudentInformation(id).ConfigureAwait(false);
            if (classDetails == null)
            {
                return null;
            }
            classDetails.Schedules = classDetails.Schedules
                                    .Where(schedule => schedule.Attendents.Any(attendent => attendent.Student.ParentId == parentId)).ToList();
            var result = _mapper.Map<ClassDetailsIncludeScheduleStudentDto>(classDetails);

            if (classDetails.Schedules.Any())
            {
                result.Schedules = _mapper.Map<IEnumerable<ScheduleStudentDto>>(classDetails.Schedules);
                foreach (var scheduleDto in result.Schedules)
                {
                    var correspondingSchedule = classDetails.Schedules.FirstOrDefault(s => s.Id == scheduleDto.Id);
                    if (correspondingSchedule != null)
                    {
                        scheduleDto.ScheduleStudentInformationDto = _mapper.Map<IEnumerable<ScheduleStudentInformationDto>>(correspondingSchedule.Attendents);
                    }
                }
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
                    var classMemberExist = await _context.ClassMembers.Where(x => x.ClassId == item.ClassId
                         && x.StudentId == item.StudentId)
                        .FirstOrDefaultAsync().ConfigureAwait(false);
                    if (classMemberExist != null && classMemberExist.Status.Equals("DELETED"))
                    {
                        classMemberExist.Status = "CREATED";
                        _context.ClassMembers.Update(classMemberExist);
                    }

                    if (classMemberExist == null)
                    {
                        var findLast = await _context.ClassMembers.OrderBy(x => x.Id).LastOrDefaultAsync().ConfigureAwait(false);
                        var classMember = _mapper.Map<ClassMember>(item);
                        classMember.Id = findLast!.Id + 1;
                        classMember.Status = "CREATED";

                        //check member
                        var membersInClass = await _context.ClassMembers.Where(x => x.ClassId == item.ClassId).ToListAsync().ConfigureAwait(false);
                        DateTime currentDate = DateTime.Now.Date;
                        if (!membersInClass.Any())
                        {
                            var schedulesOfClass = await _context.Schedules.Where(x => x.ClassId == item.ClassId && currentDate >= x.Date).ToListAsync().ConfigureAwait(false);
                            if (!schedulesOfClass.Any())
                            {
                                //add schedule attendent
                                var schedulesClass = await _context.Schedules.Where(x => x.ClassId == item.ClassId).ToListAsync().ConfigureAwait(false);
                                CreateAttendForStudent(schedulesClass, item.StudentId);
                            }
                            else
                            {
                                var findClass = await _context.Classes.Where(x => x.ClassId == item.ClassId).FirstOrDefaultAsync().ConfigureAwait(false);

                                var newSchedules = await _context.Schedules.Where(x => x.ClassId == item.ClassId).ToListAsync().ConfigureAwait(false); ;
                                _context.Schedules.RemoveRange(newSchedules);
                                await _context.SaveChangesAsync().ConfigureAwait(false);
                                newSchedules = newSchedules.DistinctBy(x => x.DayOfWeek).ToList();
                                foreach (var ns in newSchedules)
                                {
                                    DateTime nearestDate = DateTime.Now.Date;
                                    int currentDayOfWeekOrder = GetDayOfWeekOrder(ns.DayOfWeek);
                                    int todayDayOfWeekOrder = (int)DateTime.Now.DayOfWeek;

                                    if (currentDayOfWeekOrder == todayDayOfWeekOrder)
                                    {
                                        ns.Date = nearestDate;
                                    }
                                    else
                                    {
                                        while (currentDayOfWeekOrder != todayDayOfWeekOrder)
                                        {
                                            nearestDate = nearestDate.AddDays(1);
                                            todayDayOfWeekOrder = (int)nearestDate.DayOfWeek;
                                        }

                                        ns.Date = nearestDate;
                                    }
                                }

                                int count = newSchedules.Count();
                                int countStart = newSchedules.Count();
                                int countSchedules = newSchedules.Count();
                                List<Schedule> newSchedulesList = newSchedules.ToList();
                                for (int i = 0; i < findClass.NumOfSession; i++)
                                {
                                    countSchedules = newSchedulesList.Count();
                                    for (int j = countSchedules; j > countSchedules - countStart; j--)
                                    {
                                        Schedule existingSchedule = newSchedulesList[j - 1];
                                        Schedule newSchedule = new Schedule();
                                        newSchedule.Date = existingSchedule.Date.Value.AddDays(7).Date;
                                        newSchedule.ClassId = existingSchedule.ClassId;
                                        newSchedule.DayOfWeek = existingSchedule.DayOfWeek;
                                        newSchedule.SessionStart = existingSchedule.SessionStart;
                                        newSchedule.SessionEnd = existingSchedule.SessionEnd;
                                        newSchedule.Status = existingSchedule.Status;
                                        newSchedulesList.Add(newSchedule);
                                        count++;

                                        if (count == findClass.NumOfSession)
                                            break;
                                    }

                                    if (count == findClass.NumOfSession)
                                        break;
                                }
                                var lastSchedule = await _context.Schedules.OrderBy(x => x.Id).LastOrDefaultAsync().ConfigureAwait(false);
                                var newScheduleId = lastSchedule.Id + 1;
                                newSchedulesList.ToList().ForEach(s => { s.ClassId = findClass.ClassId; s.Id = newScheduleId++; });
                                await _context.Schedules.AddRangeAsync(newSchedulesList).ConfigureAwait(false);
                                await _context.SaveChangesAsync().ConfigureAwait(false);
                                //add schedule attendent
                                var schedulesClass = await _context.Schedules.Where(x => x.ClassId == item.ClassId).ToListAsync().ConfigureAwait(false);
                                CreateAttendForStudent(schedulesClass, item.StudentId);
                            }
                        }
                        else
                        {
                            //add schedule attendent
                            var schedulesOfClass = await _context.Schedules.Where(x => x.ClassId == item.ClassId && x.Date > currentDate).ToListAsync().ConfigureAwait(false);
                            CreateAttendForStudent(schedulesOfClass, item.StudentId);
                        }

                        await _context.ClassMembers.AddAsync(classMember).ConfigureAwait(false);
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async void CreateAttendForStudent(List<Schedule> schedulesOfClass, long studentId)
        {
            List<Attendent> attendents = new List<Attendent>();
            foreach (var schedule in schedulesOfClass)
            {
                Attendent attendent = new Attendent();
                attendent.StudentId = studentId;
                attendent.ScheduleId = schedule.Id;
                attendent.Attentdent = 0;
                attendents.Add(attendent);
            }
            await _context.Attendents.AddRangeAsync(attendents).ConfigureAwait(false);
        }

        public async Task<bool> DeleteStudentInClass(DeleteStudentInClassRequestDto entity)
        {
            try
            {
                var classMember = await _context.ClassMembers.Where(x => x.ClassId == entity.ClassId
                && x.StudentId == entity.StudentId)
                    .FirstOrDefaultAsync().ConfigureAwait(false);
                if (classMember == null) return false;

                classMember.Status = "DELETED";
                _context.ClassMembers.Update(classMember);

                //delete schedules
                var schedulesOfStudent = await _context.Attendents.Where(x => x.StudentId == entity.StudentId && x.Schedule.ClassId == entity.ClassId).ToListAsync().ConfigureAwait(false);
                _context.Attendents.RemoveRange(schedulesOfStudent);

                await _context.SaveChangesAsync().ConfigureAwait(false);
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
                if (entity.NumOfSession < entity.AddScheduleDto.Count()) return false;
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


                    foreach (var ns in newSchedules)
                    {
                        DateTime nearestDate = DateTime.Now.Date;
                        int currentDayOfWeekOrder = GetDayOfWeekOrder(ns.DayOfWeek);
                        int todayDayOfWeekOrder = (int)DateTime.Now.DayOfWeek;

                        if (currentDayOfWeekOrder == todayDayOfWeekOrder)
                        {
                            ns.Date = nearestDate;
                        }
                        else
                        {
                            while (currentDayOfWeekOrder != todayDayOfWeekOrder)
                            {
                                nearestDate = nearestDate.AddDays(1);
                                todayDayOfWeekOrder = (int)nearestDate.DayOfWeek;
                            }

                            ns.Date = nearestDate;
                        }
                    }

                    int count = newSchedules.Count();
                    int countStart = newSchedules.Count();
                    int countSchedules = newSchedules.Count();
                    List<Schedule> newSchedulesList = newSchedules.ToList();
                    for (int i = 0; i < entity.NumOfSession; i++)
                    {
                        countSchedules = newSchedulesList.Count();
                        for (int j = countSchedules; j > countSchedules - countStart; j--)
                        {
                            Schedule existingSchedule = newSchedulesList[j - 1];
                            Schedule newSchedule = new Schedule();
                            newSchedule.Date = existingSchedule.Date.Value.AddDays(7).Date;
                            newSchedule.ClassId = existingSchedule.ClassId;
                            newSchedule.DayOfWeek = existingSchedule.DayOfWeek;
                            newSchedule.SessionStart = existingSchedule.SessionStart;
                            newSchedule.SessionEnd = existingSchedule.SessionEnd;
                            newSchedule.Status = existingSchedule.Status;
                            newSchedulesList.Add(newSchedule);
                            count++;

                            if (count == entity.NumOfSession)
                                break;
                        }

                        if (count == entity.NumOfSession)
                            break;
                    }
                    newSchedulesList.ToList().ForEach(s => { s.ClassId = newClass.ClassId; s.Id = newScheduleId++; });
                    await _context.Schedules.AddRangeAsync(newSchedulesList).ConfigureAwait(false);
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
                var result = await _context.Classes.FirstOrDefaultAsync(c => c.ClassId.Equals(entity.ClassId) && c.Status.Equals("SUSPEND") || c.Status.Equals("COMPLETED")).ConfigureAwait(false);
                if (result == null)
                    return false;
                if (result.Status.Equals("SUSPEND") || result.Status.Equals("COMPLETED"))
                {
                    return false;
                }

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
                    var addSchedules = entity.UpdateScheduleDto.Where(s => s.Id.Equals(-1L));
                    var updateSchedule = entity.UpdateScheduleDto.Where(s => !s.Id.Equals(-1L));
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
                            oldSchedule.Date = schedule.Date;
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

        static int GetDayOfWeekOrder(string dayOfWeek)
        {
            switch (dayOfWeek.ToUpper())
            {
                case "MON":
                    return 1;
                case "TUE":
                    return 2;
                case "WED":
                    return 3;
                case "THU":
                    return 4;
                case "FRI":
                    return 5;
                case "SAT":
                    return 6;
                case "SUN":
                    return 7;
                default:
                    throw new ArgumentException("Invalid day of week: " + dayOfWeek);
            }
        }

    }
}
