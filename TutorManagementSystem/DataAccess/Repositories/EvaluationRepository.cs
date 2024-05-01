using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly ClassNTutorContext _context;

        public EvaluationRepository(ClassNTutorContext context)
        {
            _context = context;
        }
        public IQueryable<EvaluationDto> GetDetailEvaluation(long evaluationId)
        {
            var query = from e in _context.Evaluations
                        join c in _context.Classes on e.ClassId equals c.ClassId
                        join p in _context.People on e.StudentId equals p.PersonId
                        where e.EvaluationId == evaluationId
                        select new EvaluationDto
                        {
                            EvaluationId = e.EvaluationId,
                            StudentId = e.StudentId,
                            StudentName = p.FullName,
                            ClassId = e.ClassId,
                            ClassName = c.ClassName,
                            Score = e.Score,
                            Comment = e.Comment,
                            Date = e.Date,
                            CreatedAt = e.CreatedAt,
                            UpdatedAt = e.UpdatedAt,
                            Status = e.Status
                        };
            return query;
        }

        public IQueryable<EvaluationDto> GetEvaluationForParent(long parentId, long? studentId, DateTime? startDate, DateTime? endDate)
        {
            var query = from e in _context.Evaluations
                        join c in _context.Classes on e.ClassId equals c.ClassId
                        join p in _context.People on e.StudentId equals p.PersonId
                        join s in _context.Students on e.StudentId equals s.StudentId
                        where s.ParentId == parentId
                        select new EvaluationDto
                        {
                            EvaluationId = e.EvaluationId,
                            StudentId = e.StudentId,
                            StudentName = p.FullName,
                            ClassId = e.ClassId,
                            ClassName = c.ClassName,
                            Score = e.Score,
                            Comment = e.Comment,
                            Date = e.Date,
                            CreatedAt = e.CreatedAt,
                            UpdatedAt = e.UpdatedAt,
                            Status = e.Status
                        };

            if (!(studentId == null))
            {
                query = query.Where(c => c.StudentId! == studentId);
            }

            if (!startDate.Equals(null) && !endDate.Equals(null))
            {
                query = query.Where(c => startDate <= c.Date && c.Date <= endDate);
            }
            return query.OrderBy(x => x.EvaluationId);
        }

        public IQueryable<EvaluationDto> GetEvaluationForParentNoPaging(long parentId, long? classId, long? studentId, DateTime? startDate, DateTime? endDate)
        {
            var query = from e in _context.Evaluations
                        join c in _context.Classes on e.ClassId equals c.ClassId
                        join p in _context.People on e.StudentId equals p.PersonId
                        join s in _context.Students on e.StudentId equals s.StudentId
                        where s.ParentId == parentId
                        select new EvaluationDto
                        {
                            EvaluationId = e.EvaluationId,
                            StudentId = e.StudentId,
                            StudentName = p.FullName,
                            ClassId = e.ClassId,
                            ClassName = c.ClassName,
                            Score = e.Score,
                            Comment = e.Comment,
                            Date = e.Date,
                            CreatedAt = e.CreatedAt,
                            UpdatedAt = e.UpdatedAt,
                            Status = e.Status
                        };

            if (!(studentId == null))
            {
                query = query.Where(c => c.StudentId! == studentId);
            }

            if (!startDate.Equals(null) && !endDate.Equals(null))
            {
                query = query.Where(c => startDate <= c.Date && c.Date <= endDate);
            }
            if (!(classId == null))
            {
                query = query.Where(c => c.ClassId! == classId);
            }
            return query.OrderBy(x => x.Date);
        }

        public IQueryable<EvaluationDto> GetEvaluations(long classId, long? studentId, DateTime? startDate, DateTime? endDate)
        {
            var query = from e in _context.Evaluations
                        join c in _context.Classes on e.ClassId equals c.ClassId
                        join p in _context.People on e.StudentId equals p.PersonId
                        where e.ClassId == classId
                        select new EvaluationDto
                        {
                            EvaluationId = e.EvaluationId,
                            StudentId = e.StudentId,
                            StudentName = p.FullName,
                            ClassId = e.ClassId,
                            ClassName = c.ClassName,
                            Score = e.Score,
                            Comment = e.Comment,
                            Date = e.Date,
                            CreatedAt = e.CreatedAt,
                            UpdatedAt = e.UpdatedAt,
                            Status = e.Status
                        };

            if (!(studentId == null))
            {
                query = query.Where(c => c.StudentId! == studentId);
            }

            if (!startDate.Equals(null) && !endDate.Equals(null))
            {
                query = query.Where(c => startDate <= c.Date && c.Date <= endDate);
            }

            return query.OrderBy(x => x.EvaluationId);
        }

        public IQueryable<EvaluationDto> GetEvaluationsNoPaging(long classId, long? studentId, DateTime? startDate, DateTime? endDate)
        {
            var query = from e in _context.Evaluations
                        join c in _context.Classes on e.ClassId equals c.ClassId
                        join p in _context.People on e.StudentId equals p.PersonId
                        where e.ClassId == classId
                        select new EvaluationDto
                        {
                            EvaluationId = e.EvaluationId,
                            StudentId = e.StudentId,
                            StudentName = p.FullName,
                            ClassId = e.ClassId,
                            ClassName = c.ClassName,
                            Score = e.Score,
                            Comment = e.Comment,
                            Date = e.Date,
                            CreatedAt = e.CreatedAt,
                            UpdatedAt = e.UpdatedAt,
                            Status = e.Status
                        };

            if (!(studentId == null))
            {
                query = query.Where(c => c.StudentId! == studentId);
            }

            if (!startDate.Equals(null) && !endDate.Equals(null))
            {
                query = query.Where(c => startDate <= c.Date && c.Date <= endDate);
            }

            return query.OrderBy(x => x.Date);
        }
    }
}
   
