using AutoMapper;
using SEP490_BackEnd.Models;
using SEP490_BackEnd.ResponseModel.AccountProfile;

namespace SEP490_BackEnd.Mapper
{
    public class ModelsToResponseModel : Profile
    {
        public ModelsToResponseModel()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<Person, PersonResponse>();
            CreateMap<Tutor, TutorResponse>();
        }
    }
}
