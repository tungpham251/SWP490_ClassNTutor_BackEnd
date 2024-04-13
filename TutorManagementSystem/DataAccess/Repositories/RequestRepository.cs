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
                        join ps in _context.People on s.StudentId equals ps.PersonId
                        join t in _context.Tutors on r.TutorId equals t.PersonId
                        join pt in _context.People on t.PersonId equals pt.PersonId
                        where r.RequestId == id
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = ps.FullName,
                            TutorName = pt.FullName,
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

        public IQueryable<RequestDto> SearchRequestsForParent(long parentId, long subjectId, string status)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId
                        join p in _context.People on r.ParentId equals p.PersonId
                        join c in _context.Classes on r.ClassId equals c.ClassId
                        join s in _context.Students on r.StudentId equals s.StudentId
                        join ps in _context.People on s.StudentId equals ps.PersonId
                        join t in _context.Tutors on r.TutorId equals t.PersonId
                        join pt in _context.People on t.PersonId equals pt.PersonId
                        where r.ParentId == parentId
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = ps.FullName,
                            TutorName = pt.FullName,
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

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.RequestId);
        }

        public IQueryable<RequestDto> SearchRequestsForTutor(long tutorId, long subjectId, string status)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId
                        join p in _context.People on r.ParentId equals p.PersonId
                        join c in _context.Classes on r.ClassId equals c.ClassId
                        join s in _context.Students on r.StudentId equals s.StudentId
                        join ps in _context.People on s.StudentId equals ps.PersonId
                        join t in _context.Tutors on r.TutorId equals t.PersonId
                        join pt in _context.People on t.PersonId equals pt.PersonId
                        where r.TutorId == tutorId
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = ps.FullName,
                            TutorName = pt.FullName,
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

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.RequestId);
        }
    }
}
