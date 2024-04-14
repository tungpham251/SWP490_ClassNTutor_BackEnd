using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private readonly ClassNTutorContext _context;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3storageService;

        public StudentService(ClassNTutorContext context, IClassRepository classRepository, IMapper mapper, IS3StorageService s3storageService)
        {
            _context = context;
            _mapper = mapper;
            _s3storageService = s3storageService;
        }

        public async Task<bool> AddStudent(AddStudentDto entity)
        {
            try
            {
                var avatar = "";
                var newStudent = _mapper.Map<Student>(entity);
                var newPerson = _mapper.Map<Person>(entity);
                var lastPerson = await _context.People.OrderBy(x => x.PersonId).LastOrDefaultAsync().ConfigureAwait(false);
                var lastStudent = await _context.Students.OrderBy(x => x.StudentId).LastOrDefaultAsync().ConfigureAwait(false);
                newStudent.StudentId = lastStudent.StudentId + 1;
                newPerson.PersonId = lastPerson.PersonId + 1;
                if (entity.Avatar != null)
                {
                    avatar = await _s3storageService.UploadFileToS3(entity.Avatar!).ConfigureAwait(false);
                    newPerson.UserAvatar = avatar;
                }
                await _context.People.AddAsync(newPerson).ConfigureAwait(false);
                await _context.Students.AddAsync(newStudent).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<bool> DeleteStudent(long id)
        {
            try
            {
                var oldStudent = await _context.Students.FirstOrDefaultAsync(s => s.StudentId.Equals(id))
                                                       .ConfigureAwait(false);
                if (oldStudent == null)
                    return false;
                _context.Students.Remove(oldStudent);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStudent(UpdateStudentDto entity)
        {
            try
            {
                var oldStudent = await _context.Students.FirstOrDefaultAsync(s => s.StudentId.Equals(entity.StudentId))
                                                        .ConfigureAwait(false);
                var oldPerson = await _context.People.FirstOrDefaultAsync(s => s.PersonId.Equals(entity.PersonId))
                                                        .ConfigureAwait(false);
                if (oldStudent == null || oldPerson == null || oldStudent.StudentId != oldPerson.PersonId)
                    return false;
                var avatar = "";
                if (entity.UserAvatar != null)
                {
                    avatar = await _s3storageService.UploadFileToS3(entity.UserAvatar!).ConfigureAwait(false);
                    oldPerson.UserAvatar = avatar;
                }
                if (entity.ParentId != null)
                {
                    oldStudent.ParentId = entity.ParentId;
                }
                if (entity.StudentLevel != null)
                {
                    oldStudent.StudentLevel = entity.StudentLevel;
                }
                if (entity.Status != null)
                {
                    oldStudent.Status = entity.Status;
                }
                if (entity.PersonId != null)
                {
                    oldPerson.PersonId = entity.PersonId;
                }
                if (entity.FullName != null)
                {
                    oldPerson.FullName = entity.FullName;
                }
                if (entity.Phone != null)
                {
                    oldPerson.Phone = entity.Phone;
                }
                if (entity.Gender != null)
                {
                    oldPerson.Gender = entity.Gender;
                }
                if (entity.Address != null)
                {
                    oldPerson.Address = entity.Address;
                }
                if (entity.Dob != null)
                {
                    oldPerson.Dob = entity.Dob;
                }
                _context.Students.Update(oldStudent);
                _context.People.Update(oldPerson);
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
