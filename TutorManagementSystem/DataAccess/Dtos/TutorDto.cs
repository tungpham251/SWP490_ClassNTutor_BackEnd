using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{

    public class TutorRequestDto
    {
        public string? subjectName { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
    }

}
