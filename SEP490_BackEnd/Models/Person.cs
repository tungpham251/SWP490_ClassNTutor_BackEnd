using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Person
{
    public long PersonId { get; set; }

    public string FullName { get; set; } = null!;

    public string? UserAvatar { get; set; }

    public string Phone { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public virtual Account PersonNavigation { get; set; } = null!;

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Tutor? Tutor { get; set; }
}
