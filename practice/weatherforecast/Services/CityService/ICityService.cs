﻿using weatherforecast.Dto;
using weatherforecast.Models;

namespace weatherforecast.Services.CityService
{
    public interface ICityService
    {
        Task<ServiceRepsonse<List<GetCityDto>>> GetAllCities();
        Task<ServiceRepsonse<GetCityDto>> GetCityById(int cityId);
        Task<ServiceRepsonse<List<GetCityDto>>> AddCity(AddCityDto character);
        Task<ServiceRepsonse<GetCityDto>> UpdateCity(UpdateCityDto updateCharacter, int cityId);
        Task<ServiceRepsonse<string>> DeleteCity(int cityId);

    }
}
