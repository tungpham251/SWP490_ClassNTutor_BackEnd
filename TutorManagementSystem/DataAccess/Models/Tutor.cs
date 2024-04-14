using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Tutor
    {
        public Tutor()
        {
            Classes = new HashSet<Class>();
            Payments = new HashSet<Payment>();
            SubjectTutors = new HashSet<SubjectTutor>();
        }

        public long PersonId { get; set; }
        public string Cmnd { get; set; } = null!;
        public string FrontCmnd { get; set; } = null!;
        public string BackCmnd { get; set; } = null!;
        public string Cv { get; set; } = null!;
        public string EducationLevel { get; set; } = null!;
        public string School { get; set; } = null!;
        public string GraduationYear { get; set; } = null!;
        public string? About { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<SubjectTutor> SubjectTutors { get; set; }
    }
}
