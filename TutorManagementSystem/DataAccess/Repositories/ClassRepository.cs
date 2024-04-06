using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ClassNTutorContext _context;

        public ClassRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<ClassDto> GetClassDetail(long id)
        {
            var query = from c in _context.Classes
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        where c.ClassId == id
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
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


        public IQueryable<ClassDto> SearchClass(string searchWord, string status)
        {
            var query = from c in _context.Classes
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
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

        public IQueryable<ClassDto> SearchClassOfTutor(int tutorId, string searchWord, string status)
        {
            var query = from c in _context.Classes
                        join sj in _context.Subjects on c.SubjectId equals sj.SubjectId
                        where c.TutorId == tutorId
                        select new ClassDto
                        {
                            ClassId = c.ClassId,
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
    }
}
