using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class SubjectTutorDto
    {
        public long SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int Level { get; set; }
        public string Status { get; set; } = null!;
    }
    public class AddSubjectTutorDto
    {
        public long SubjectId { get; set; }
        public long TutorId { get; set; }
        public int Level { get; set; }
        public string Status { get; set; } = null!;
    }
}
