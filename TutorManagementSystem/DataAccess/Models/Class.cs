using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Class
    {
        public Class()
        {
            ClassMembers = new HashSet<ClassMember>();
            Evaluations = new HashSet<Evaluation>();
            Requests = new HashSet<Request>();
            Schedules = new HashSet<Schedule>();
        }

        public long ClassId { get; set; }
        public long TutorId { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDesc { get; set; }
        public int ClassLevel { get; set; }
        public long Price { get; set; }
        public long SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual Tutor Tutor { get; set; } = null!;
        public virtual ICollection<ClassMember> ClassMembers { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
