using AutoMapper;
using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<Evaluation, EvaluationDto>();
            CreateMap<ClassMember, AddStudentInClassRequestDto>();
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.StudentNavigation.Address))
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.StudentNavigation.Dob))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.StudentNavigation.UserAvatar))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.StudentNavigation == null ? "null" : src.StudentNavigation.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.StudentNavigation.Gender))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.StudentNavigation.Phone))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent == null ? "null" : src.Parent.FullName));

            CreateMap<Class, ClassDto>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName));
            CreateMap<Class, AddClassDto>();
            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class!.ClassName))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Email))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName));
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account!.Email))
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Account!.Role.RoleName))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Account!.Status));


            CreateMap<Class, ClassDetailsIncludeStudentInfoDto>()
                .ForMember(dest => dest.TutorName, opt => opt.MapFrom(src => src.Tutor == null ? "null" : src.Tutor.Person == null ? "null" : src.Tutor.Person.FullName))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject == null ? "null" : src.Subject == null ? "null" : src.Subject.SubjectName));
            CreateMap<ClassMember, StudentInformationDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.FullName))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.UserAvatar))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.Phone))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.Gender))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.Address))                              
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.Parent == null ? "null" : src.Student.Parent.FullName))
                .ForMember(dest => dest.ParentPhone, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.Parent == null ? "null" : src.Student.Parent.Phone))
                .ForMember(dest => dest.StudentLevel, opt => opt.MapFrom(src => src.Student == null ? -1 : src.Student.StudentLevel))
                .ForMember(dest => dest.StatusClassMember, opt => opt.MapFrom(src => src.Status));
                
            CreateMap<Payment, ResponsePaymentDto>()
                .ForMember(dest => dest.PayerName, opt => opt.MapFrom(src => src.Payer == null ? "null" : src.Payer.Person == null ? "null" : src.Payer.Person.FullName))
                .ForMember(dest => dest.RequestName, opt => opt.MapFrom(src => src.Request == null ? "null" : src.Request.Person == null ? "null" : src.Request.Person.FullName))
                ;

            CreateMap<Schedule, ScheduleDto>();
            CreateMap<SubjectTutor, SubjectTutorDto>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject == null ? "null" : src.Subject.SubjectName));

            CreateMap<Person, ProfileDto>();
            CreateMap<Tutor, GetTutorDto>();
            CreateMap<Account, AccountDto>().ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<Staff, GetStaffDto>();
        }

    }
}
