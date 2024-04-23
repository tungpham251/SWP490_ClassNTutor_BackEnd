﻿using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        IQueryable<FilterScheduleDto> FilterScheduleTutor(DateTime? from, DateTime? to, long classId, long personId);
        IQueryable<FilterScheduleDto> FilterScheduleParent(DateTime? from, DateTime? to, long classId, long personId, string studentName);
    }

}