using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class SubjectTutor
    {
        public long SubjectId { get; set; }
        public long TutorId { get; set; }
        public int Level { get; set; }
        public string Status { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual Tutor Tutor { get; set; } = null!;
    }
}
