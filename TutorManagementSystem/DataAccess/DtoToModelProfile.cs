using AutoMapper;
using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess
{
    public class DtoToModelProfile : Profile
    {

        public DtoToModelProfile()
        {
            CreateMap<RoleDto, Role>();
            CreateMap<SubjectDto, Subject>();
            CreateMap<EvaluationDto, Evaluation>();
            CreateMap<AddEvaluationDto, Evaluation>();
            CreateMap<ClassDto, Class>();
            CreateMap<AddClassDto, Class>();
            CreateMap<AddClassIncludeScheduleDto, Class>();
            CreateMap<UpdateClassIncludeScheduleDto, Class>();
            CreateMap<UpdateScheduleDto, Schedule>();
            CreateMap<AddScheduleDto, Schedule>();
            CreateMap<AddRequestDto, Request>();
            CreateMap<UpdateRequestDto, Request>();
            CreateMap<AddStudentDto, Student>();
            CreateMap<AddStudentDto, Person>();
            CreateMap<AddSubjectTutorDto, SubjectTutor>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<AddStudentInClassRequestDto, ClassMember>();
            CreateMap<AttendentRequestDto, Attendent>();

        }

    }
}
