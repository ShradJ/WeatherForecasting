using AutoMapper;

using weatherforecast.Dto;
using weatherforecast.Models;

namespace weatherforecast
{
    public class AutoMapperProfiless : Profile
    {
        public AutoMapperProfiless()
        {
            CreateMap<City, GetCityDto>();
            CreateMap<City, AddCityDto>();
            CreateMap<GetCityDto, City>();
            CreateMap<AddCityDto, City>();
            CreateMap<Measurement, GetMeasurementDto>();
            CreateMap<Measurement, AddMeasurementDto>();
            CreateMap<GetMeasurementDto, Measurement>();
            CreateMap<AddMeasurementDto, Measurement>();
        }
    }
}
