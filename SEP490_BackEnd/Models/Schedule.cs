using System;
using System.Collections.Generic;

namespace SEP490_BackEnd.Models;

public partial class Schedule
{
    public long Id { get; set; }

    public long ClassId { get; set; }

    public string DaysOfWeek { get; set; } = null!;

    public TimeSpan SessionStart { get; set; }

    public TimeSpan SessionEnd { get; set; }

    public virtual Class Class { get; set; } = null!;
}
