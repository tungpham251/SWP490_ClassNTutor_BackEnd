﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class StudentDto
    {
        public long StudentId { get; set; }
        public long ParentId { get; set; }
        public string? ParentName { get; set; }
        public int StudentLevel { get; set; }
        public string? Status { get; set; } = null;
        public string FullName { get; set; } = null!;
        public string? UserAvatar { get; set; }
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime? Dob { get; set; }
    }

    public class StudentInClassRequestDto
    {
        public string? SearchWord { get; set; }
        [Required]
        public PagingRequest PagingRequest { get; set; } = null!;
    }

    public class AddStudentInClassRequestDto
    {
        public long Id { get; set; }
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public string Status { get; set; } = null!;
    }

    public class AddStudentDto
    {
        public long StudentId { get; set; }
        public long ParentId { get; set; }
        public int StudentLevel { get; set; }
        public string? Status { get; set; }
    }

    public class UpdateStudentDto
    {
        public long StudentId { get; set; }
        public long ParentId { get; set; }
        public int StudentLevel { get; set; }
        public string? Status { get; set; }
    }
}
