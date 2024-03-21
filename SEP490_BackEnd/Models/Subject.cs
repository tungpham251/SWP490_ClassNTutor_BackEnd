using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Subject
{
    public long SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public string? SubjectDesc { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
