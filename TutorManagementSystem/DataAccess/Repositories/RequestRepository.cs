using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Net.NetworkInformation;

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
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId into sjJoin
                        from sj in sjJoin.DefaultIfEmpty()
                        join p in _context.People on r.ParentId equals p.PersonId into pJoin
                        from p in pJoin.DefaultIfEmpty()
                        join c in _context.Classes on r.ClassId equals c.ClassId into cJoin
                        from c in cJoin.DefaultIfEmpty()
                        join s in _context.People on r.StudentId equals s.PersonId into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        join t in _context.People on r.TutorId equals t.PersonId into tJoin
                        from t in tJoin.DefaultIfEmpty()
                        where r.RequestId == id
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = s != null ? s.FullName : "",
                            TutorName = t != null ? t.FullName : "",
                            ParentName = p != null ? p.FullName : "",
                            RequestType = r.RequestType,
                            ClassName = c != null ? c.ClassName : "",
                            Level = r.Level,
                            SubjectName = sj != null ? sj.SubjectName : "",
                            SubjectId = sj != null ? sj.SubjectId : 0,
                            Price = r.Price,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Status = r.Status
                        };

            return query;
        }

        public IQueryable<RequestDto> GetRequests(string status)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId
                        join p in _context.People on r.ParentId equals p.PersonId
                        join c in _context.Classes on r.ClassId equals c.ClassId
                        join s in _context.People on r.StudentId equals s.PersonId
                        join t in _context.People on r.TutorId equals t.PersonId
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = s.FullName,
                            TutorName = t.FullName,
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

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            return query.OrderBy(x => x.RequestId);
        }

        public IQueryable<RequestDto> SearchRequestsForParent(long parentId, string status, string requestType)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId into sjJoin
                        from sj in sjJoin.DefaultIfEmpty()
                        join p in _context.People on r.ParentId equals p.PersonId into pJoin
                        from p in pJoin.DefaultIfEmpty()
                        join c in _context.Classes on r.ClassId equals c.ClassId into cJoin
                        from c in cJoin.DefaultIfEmpty()
                        join s in _context.People on r.StudentId equals s.PersonId into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        join t in _context.People on r.TutorId equals t.PersonId into tJoin
                        from t in tJoin.DefaultIfEmpty()
                        where r.ParentId == parentId
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = s != null ? s.FullName : "",
                            TutorName = t != null ? t.FullName : "",
                            ParentName = p != null ? p.FullName : "",
                            RequestType = r.RequestType,
                            ClassName = c != null ? c.ClassName : "",
                            Level = r.Level,
                            SubjectName = sj != null ? sj.SubjectName : "",
                            SubjectId = sj != null ? sj.SubjectId : 0,
                            Price = r.Price,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Status = r.Status
                        };

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }
            if (!string.IsNullOrWhiteSpace(requestType))
            {
                query = query.Where(c => c.RequestType!.Equals(requestType));
            }

            return query.OrderBy(x => x.RequestId);
        }


        public IQueryable<RequestDto> SearchRequestsForTutor(long tutorId, string status, string requestType)
        {
            var query = from r in _context.Requests
                        join sj in _context.Subjects on r.SubjectId equals sj.SubjectId into sjJoin
                        from sj in sjJoin.DefaultIfEmpty()
                        join p in _context.People on r.ParentId equals p.PersonId into pJoin
                        from p in pJoin.DefaultIfEmpty()
                        join c in _context.Classes on r.ClassId equals c.ClassId into cJoin
                        from c in cJoin.DefaultIfEmpty()
                        join s in _context.People on r.StudentId equals s.PersonId into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        join t in _context.People on r.TutorId equals t.PersonId into tJoin
                        from t in tJoin.DefaultIfEmpty()
                        where r.TutorId == tutorId
                        && !r.Status!.Equals("CANCELLED")
                        select new RequestDto
                        {
                            RequestId = r.RequestId,
                            StudentName = s != null ? s.FullName : "",
                            TutorName = t != null ? t.FullName : "",
                            ParentName = p != null ? p.FullName : "",
                            RequestType = r.RequestType,
                            ClassName = c != null ? c.ClassName : "",
                            Level = r.Level,
                            SubjectName = sj != null ? sj.SubjectName : "",
                            SubjectId = sj != null ? sj.SubjectId : 0,
                            Price = r.Price,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            Status = r.Status
                        };

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status!.Equals(status));
            }

            if (!string.IsNullOrWhiteSpace(requestType))
            {
                query = query.Where(c => c.RequestType!.Equals(requestType));
            }
            return query.OrderBy(x => x.RequestId);
        }
    }
}
