using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Tutor
{
    public long PersonId { get; set; }

    public string Cmnd { get; set; } = null!;

    public string FrontCmnd { get; set; } = null!;

    public string BackCmnd { get; set; } = null!;

    public string Cv { get; set; } = null!;

    public string EducationLevel { get; set; } = null!;

    public string School { get; set; } = null!;

    public string GraduationYear { get; set; } = null!;

    public string? About { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Person Person { get; set; } = null!;
}
