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
            CreateMap<ClassDto, Class>();
            CreateMap<AddClassDto, Class>();
            CreateMap<AddRequestDto, Request>();
        }

    }
}
