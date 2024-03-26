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
        }

    }
}
