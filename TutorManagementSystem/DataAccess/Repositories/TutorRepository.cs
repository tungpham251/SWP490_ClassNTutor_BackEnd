using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly ClassNTutorContext _context;

        public TutorRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<GetTutorDto> SearchTutors(string subjectName)
        {
            if (string.IsNullOrEmpty(subjectName))
                subjectName = "";
            subjectName = subjectName.Trim();

            var query = from st in _context.SubjectTutors
                        join s in _context.Subjects on st.SubjectId equals s.SubjectId
                        join t in _context.Tutors on st.TutorId equals t.PersonId
                        join p in _context.People on t.PersonId equals p.PersonId
                        join a in _context.Accounts on p.PersonId equals a.PersonId
                        where a.Status.Equals("ACTIVE") && s.SubjectName.ToLower().Contains(subjectName.ToLower())
                        select new GetTutorDto
                        {
                            PersonId = t.PersonId,
                            Cmnd = t.Cmnd,
                            FrontCmnd = t.FrontCmnd,
                            BackCmnd = t.BackCmnd,
                            Cv = t.Cv,
                            EducationLevel = t.EducationLevel,
                            School = t.School,
                            GraduationYear = t.GraduationYear,
                            About = t.About,
                            FullName = p.FullName,
                            UserAvatar = p.UserAvatar,
                            Phone = p.Phone,
                            Gender = p.Gender,
                            Address = p.Address,
                            Dob = p.Dob
                        };
            return query;
        }
    }
}
