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
            CreateMap<Class, ClassDto>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName));
            CreateMap<Class, AddClassDto>();
            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Email))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName));
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email));

            CreateMap<Class, ClassDetailsIncludeStudentInfoDto>();
            CreateMap<ClassMember, StudentInformationDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.FullName))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.UserAvatar))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.Phone))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.Gender))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Student == null ? "null" : src.Student.StudentNavigation == null ? "null" : src.Student.StudentNavigation.Address));

            CreateMap<Payment, ResponsePaymentDto>();
        }

    }
}
