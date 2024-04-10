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
                if (oldStudent == null)
                    return false;
                oldStudent.ParentId = entity.ParentId;
                oldStudent.StudentLevel = entity.StudentLevel;
                oldStudent.Status = entity.Status;
                _context.Students.Update(oldStudent);
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
