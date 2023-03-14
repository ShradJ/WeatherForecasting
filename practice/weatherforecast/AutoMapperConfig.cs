using AutoMapper;

namespace weatherforecast
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var coreAssembly = typeof(AutoMapperConfig).Assembly;
            return new MapperConfiguration(mc=>mc.AddMaps(coreAssembly)).CreateMapper();
        }
    }
}
