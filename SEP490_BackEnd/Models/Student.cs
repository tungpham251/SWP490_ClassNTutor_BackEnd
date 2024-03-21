using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Student
{
    public long StudentId { get; set; }

    public long PersonId { get; set; }

    public string StudentName { get; set; } = null!;

    public int StudentLevel { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual Person Person { get; set; } = null!;
}
