using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Student
    {
        public Student()
        {
            ClassMembers = new HashSet<ClassMember>();
            Evaluations = new HashSet<Evaluation>();
            Requests = new HashSet<Request>();
        }

        public long StudentId { get; set; }
        public long ParentId { get; set; }
        public int StudentLevel { get; set; }
        public string? Status { get; set; }

        public virtual Person Parent { get; set; } = null!;
        public virtual Person StudentNavigation { get; set; } = null!;
        public virtual ICollection<ClassMember> ClassMembers { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
