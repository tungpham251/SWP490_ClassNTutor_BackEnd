using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ClassMember
    {
        public long Id { get; set; }
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
