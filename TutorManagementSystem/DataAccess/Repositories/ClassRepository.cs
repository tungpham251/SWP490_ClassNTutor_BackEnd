﻿using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ClassNTutorContext _context;
        private const int STAFF = 0;
        private const int TUTOR = 1;
        public ClassRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<ClassDto> GetClassDetail(long id)
        {
            var query = from c in _context.Classes
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        join p in _context.People on c.TutorId equals p.PersonId
                        where c.ClassId == id
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
                            TutorName = p.FullName,
                            ClassName = c.ClassName,
                            ClassDesc = c.ClassDesc,
                            ClassLevel = c.ClassLevel,
                            Price = c.Price,
                            SubjectName = sj.SubjectName,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            MaxCapacity = c.MaxCapacity,
                            CreatedAt = c.CreatedAt,
                            UpdatedAt = c.UpdatedAt,
                            Status = c.Status
                        };
            return query;
        }


        public IQueryable<ClassDto> SearchClass(string searchWord, string status, long? subjectId)
        {
            var query = from c in _context.Classes
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        join p in _context.People on c.TutorId equals p.PersonId
                        join t in _context.Tutors on c.TutorId equals t.PersonId
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
                            TutorName = p.FullName,
                            UserAvatar = p.UserAvatar!,
                            ClassName = c.ClassName,
                            TutorId = c.TutorId,
                            School = t.School,
                            EducationLevel = t.EducationLevel,
                            GraduationYear = t.GraduationYear,
                            ClassDesc = c.ClassDesc,
                            ClassLevel = c.ClassLevel,
                            Price = c.Price,
                            SubjectName = sj.SubjectName,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            MaxCapacity = c.MaxCapacity,
                            CreatedAt = c.CreatedAt,
                            UpdatedAt = c.UpdatedAt,
                            Status = c.Status
                        };

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(x => x.ClassName!.ToLower().Contains(searchWord.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            if (subjectId != null)
            {
                var subject = _context.Subjects.Where(x => x.SubjectId.Equals(subjectId)).FirstOrDefault();
                query = query.Where(c => c.SubjectName == subject.SubjectName);
            }
            return query.OrderBy(x => x.ClassId);

        }

        public IQueryable<ClassDto> SearchClassForTutor(long tutorId, string searchWord, string status)
        {
            var query = from c in _context.Classes
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        join p in _context.People on c.TutorId equals p.PersonId
                        where c.TutorId == tutorId
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
                            TutorName = p.FullName,
                            TutorId = c.TutorId,
                            ClassName = c.ClassName,
                            ClassDesc = c.ClassDesc,
                            ClassLevel = c.ClassLevel,
                            Price = c.Price,
                            SubjectName = sj.SubjectName,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            MaxCapacity = c.MaxCapacity,
                            CreatedAt = c.CreatedAt,
                            UpdatedAt = c.UpdatedAt,
                            Status = c.Status
                        };

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(x => x.ClassName!.ToLower().Contains(searchWord.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.ClassId);
        }

        public IQueryable<ClassDto> SearchClassForParent(long parentId, string searchWord, string status)
        {
            var query = from c in _context.Classes
                        join p in _context.People on c.TutorId equals p.PersonId
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        join cm in _context.ClassMembers on c.ClassId equals cm.ClassId
                        join s in _context.Students on cm.StudentId equals s.StudentId
                        where s.ParentId == parentId
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
                            TutorName = p.FullName,
                            TutorId = c.TutorId,
                            ClassName = c.ClassName,
                            ClassDesc = c.ClassDesc,
                            ClassLevel = c.ClassLevel,
                            Price = c.Price,
                            SubjectName = sj.SubjectName,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            MaxCapacity = c.MaxCapacity,
                            CreatedAt = c.CreatedAt,
                            UpdatedAt = c.UpdatedAt,
                            Status = c.Status
                        };

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(x => x.ClassName!.ToLower().Contains(searchWord.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.ClassId);
        }

        public void UpdateClass(Class entity)
        {
            _context.Classes.Update(entity);
        }

        public void DeleteClassById(long personId, long classId)
        {
            var currentUser = _context.People.Include(p => p.Account)
                                                   .ThenInclude(a => a.Role)
                                                   .FirstOrDefault(p => p.PersonId.Equals(personId));                                                   

            var classById = _context.Classes.FirstOrDefault(c => c.ClassId.Equals(classId));
            if(classById.Status.Equals("ACTIVE"))
            {
                if (currentUser.Account.RoleId.Equals(STAFF))
                {
                    classById.ClassName += "-SUSPEND";
                    classById.ClassDesc = "SUSPEND";
                }
                classById.Status = "SUSPEND";
            }else if(classById.Status.Equals("SUSPEND"))
            {
                if(currentUser.Account.RoleId.Equals(STAFF))
                {
                    if (classById.ClassDesc.Equals("SUSPEND"))
                    {
                        classById.Status = "ACTIVE";
                        classById.ClassDesc = "";
                        classById.ClassName = classById.ClassName.Substring(0, classById.ClassName.Length - "-SUSPEND".Length);
                    }
                }
                else if(currentUser.Account.RoleId.Equals(TUTOR))
                {
                    if (!classById.ClassDesc.Equals("SUSPEND"))
                    {
                        classById.Status = "ACTIVE";                       
                    }
                }
                
                
            }
            
            _context.Classes.Update(classById);
        }

        public async Task<Class> GetClassByIdIncludeStudentInformation(long id)
        {
            var result = await _context.Classes
                             .Include(c => c.Tutor).ThenInclude(c=>c.Person)
                             .Include(c => c.Subject)
                             .Include(c => c.Schedules)
                             .Include(c => c.ClassMembers)
                             .ThenInclude(c => c.Student)
                             .ThenInclude(c => c.Parent).Include(c => c.ClassMembers).ThenInclude(c => c.Student)
                             .ThenInclude(c => c.StudentNavigation)
                 .FirstOrDefaultAsync(c => c.ClassId.Equals(id) && c.Status.Equals("ACTIVE")).ConfigureAwait(false);
            return result;
        }

        public async Task<Class> GetClassByIdIncludeScheduleStudentInformation(long id)
        {
            var result = await _context.Classes
                             .Include(c => c.Tutor).ThenInclude(c => c.Person)
                             .Include(c => c.Subject)
                             .Include(c => c.Schedules)
                             .ThenInclude(c => c.Attendents)
                             .ThenInclude(c => c.Student)
                             .ThenInclude(c => c.StudentNavigation)
                 .FirstOrDefaultAsync(c => c.ClassId.Equals(id) && c.Status.Equals("ACTIVE")).ConfigureAwait(false);
            return result;
        }
        public IQueryable<StudentDto> SearchStudentInParent(string searchWord)
        {
            var query = from s in _context.Students
                        join p in _context.People on s.StudentId equals p.PersonId
                        join pr in _context.People on s.ParentId equals pr.PersonId
                        select new StudentDto
                        {
                            StudentId = s.StudentId,
                            ParentId = s.ParentId,
                            ParentName = pr.FullName,
                            StudentLevel = s.StudentLevel,
                            Status = s.Status,
                            FullName = p.FullName,
                            UserAvatar = p.UserAvatar,
                            Phone = p.Phone,
                            Gender = p.Gender,
                            Address = p.Address,
                            Dob = p.Dob
                        };

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(x => x.Phone!.ToLower().Contains(searchWord.ToLower())
                || x.FullName!.ToLower().Contains(searchWord.ToLower()));
            }

            return query.OrderBy(x => x.StudentId);
        }
    }
}
