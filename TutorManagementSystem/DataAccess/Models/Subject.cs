using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Classes = new HashSet<Class>();
            Requests = new HashSet<Request>();
        }

        public long SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
