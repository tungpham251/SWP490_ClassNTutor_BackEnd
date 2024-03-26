using AutoMapper;

namespace DataAccess
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelToDtoProfile>();
                cfg.AddProfile<DtoToModelProfile>();
            });

            return config.CreateMapper();
        }
    }
}
