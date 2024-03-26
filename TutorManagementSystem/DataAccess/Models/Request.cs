using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Request
    {
        public long RequestId { get; set; }
        public long ParentId { get; set; }
        public long TutorId { get; set; }
        public long StudentId { get; set; }
        public string RequestType { get; set; } = null!;
        public long? ClassId { get; set; }
        public int? Level { get; set; }
        public long SubjectId { get; set; }
        public long? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Class? Class { get; set; }
        public virtual Account Parent { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Account Tutor { get; set; } = null!;
    }
}
