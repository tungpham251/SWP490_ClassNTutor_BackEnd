using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ClassNTutorContext _context;

        public RequestRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<RequestDto> GetRequestDetail(long id)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId
                        join p in _context.People on r.ParentId equals p.PersonId
                        join c in _context.Classes on r.ClassId equals c.ClassId
                        join s in _context.Students on r.StudentId equals s.StudentId
                        where r.RequestId == id
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentId = s.StudentId,
                            ParentName = p.FullName,
                            RequestType = r.RequestType,
                            ClassName = c.ClassName,
                            Level = r.Level,
                            SubjectName = sj.SubjectName,
                            SubjectId = sj.SubjectId,
                            Price = r.Price,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Status = r.Status
                        };
            return query;
        }

        public IQueryable<RequestDto> SearchRequest(int subjectId)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId
                        join p in _context.People on r.ParentId equals p.PersonId
                        join c in _context.Classes on r.ClassId equals c.ClassId
                        join s in _context.Students on r.StudentId equals s.StudentId
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentId = s.StudentId,
                            ParentName = p.FullName, 
                            RequestType = r.RequestType,
                            ClassName = c.ClassName,
                            Level = r.Level,
                            SubjectName = sj.SubjectName,
                            SubjectId = sj.SubjectId,
                            Price = r.Price,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Status = r.Status
                        };

            if (subjectId >= 0)
            {
                query = query.Where(x => x.SubjectId == subjectId);
            }

            return query.OrderBy(x => x.RequestId);
        }

        public IQueryable<RequestDto> SearchRequestOfTutor(int tutorId, int subjectId)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId
                        join p in _context.People on r.ParentId equals p.PersonId
                        join c in _context.Classes on r.ClassId equals c.ClassId
                        join s in _context.Students on r.StudentId equals s.StudentId
                        where r.TutorId == tutorId
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentId = s.StudentId,
                            ParentName = p.FullName,
                            RequestType = r.RequestType,
                            ClassName = c.ClassName,
                            Level = r.Level,
                            SubjectName = sj.SubjectName,
                            SubjectId = sj.SubjectId,
                            Price = r.Price,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Status = r.Status
                        };

            if (subjectId >= 0)
            {
                query = query.Where(x => x.SubjectId == subjectId);
            }

            return query.OrderBy(x => x.RequestId);
        }
    }
}
