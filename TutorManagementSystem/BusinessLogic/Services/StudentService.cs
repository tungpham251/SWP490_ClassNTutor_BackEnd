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

        public StudentService(ClassNTutorContext context, IClassRepository classRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddStudent(AddStudentDto entity)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(entity);
                var newPerson = _mapper.Map<Person>(entity);
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
                oldStudent.ParentId = entity.ParentId;
                oldStudent.StudentLevel = entity.StudentLevel;
                oldStudent.Status = entity.Status;

                oldPerson.PersonId = entity.PersonId;
                oldPerson.FullName = entity.FullName;
                oldPerson.UserAvatar = entity.UserAvatar;
                oldPerson.Phone = entity.Phone;
                oldPerson.Gender = entity.Gender;
                oldPerson.Address = entity.Address;
                oldPerson.Dob = entity.Dob;
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
