using AutoMapper;

using Microsoft.EntityFrameworkCore;

using weatherforecast.Data;
using weatherforecast.Dto;
using weatherforecast.Models;

namespace weatherforecast.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        public WeatherDataContext _context;

        public CityService(IMapper mapper, WeatherDataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceRepsonse<List<GetCityDto>>> AddCity(AddCityDto cityToBeAdded)
        {
            var serviceResponse = new ServiceRepsonse<List<GetCityDto>>();
            var city = _mapper.Map<City>(cityToBeAdded);

            city.CityName = cityToBeAdded.CityName;
            _context.City.Add(city);

            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context
                                         .City
                                         .Select(c => _mapper
                                                        .Map<GetCityDto>(c)
                                                 )
                                         .ToListAsync();
            return serviceResponse;
        }
        public City AddCityWithoutAsync(AddCityDto cityToBeAdded)
        {
            var city = _mapper.Map<City>(cityToBeAdded);
            city.CityName = cityToBeAdded.CityName;
            _context.City.Add(city);
            _context.SaveChangesAsync();
            return city;

        }

        public async Task<ServiceRepsonse<string>> DeleteCity(int cityId)
        {
            var serviceResponse = new ServiceRepsonse<string>();
            var city = await _context
                            .City
                            .FirstOrDefaultAsync(c => c.CityId == cityId);

            if (city == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"City with cityId = {cityId} not found";
                return serviceResponse;
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            serviceResponse.Data = $"City with cityId {city.CityId} is deleted.";
            return serviceResponse;
        }

        public async Task<ServiceRepsonse<List<GetCityDto>>> GetAllCities()
        {
            var serviceResponse = new ServiceRepsonse<List<GetCityDto>>();
            var cities = await _context.City.ToListAsync();
            foreach (City city in cities)
            {
                var measurements = await _context
                                            .Measurement
                                            .Where(m => m.CityId == city.CityId)
                                            .ToListAsync();

                city.Measurements = measurements.Select(m => _mapper.Map<GetMeasurementDto>(m)).ToList();
            }
            serviceResponse.Data = cities
                                    .Select(c => _mapper.Map<GetCityDto>(c))
                                    .ToList();
            return serviceResponse;
        }

        public async Task<ServiceRepsonse<GetCityDto>> GetCityById(int cityId)
        {
            var serviceResponse = new ServiceRepsonse<GetCityDto>();
            var city = await _context.City.FirstOrDefaultAsync(c => c.CityId == cityId);

            if (city is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"City with cityId = {cityId} not found.";
                return serviceResponse;
                //throw new Exception($"City with cityId = {cityId} not found.");
            }

            var measurements = await _context.Measurement
                                     .Where(m => m.CityId == cityId)
                                     .ToListAsync();

            city.Measurements = measurements
                                .Select(m => _mapper.Map<GetMeasurementDto>(m))
                                .ToList();
            serviceResponse.Data = _mapper.Map<GetCityDto>(city);
            return serviceResponse;
        }

        public async Task<ServiceRepsonse<GetCityDto>> UpdateCity(UpdateCityDto updateCity, int cityId)
        {
            var serviceResponse = new ServiceRepsonse<GetCityDto>();
            try
            {
                var city = await _context.City
                                         .FirstOrDefaultAsync(c => c.CityId == cityId);

                if (city is null)
                {
                    throw new Exception($"City with cityId = {cityId} not found.");
                }

                city.CityName = updateCity.CityName;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCityDto>(city);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
