using System.Net;

using Microsoft.AspNetCore.Mvc;

using weatherforecast.Dto;
using weatherforecast.Models;
using weatherforecast.Services.CityService;

namespace weatherforecast.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceRepsonse<List<GetCityDto>>>> Get()
        {
            return Ok(await _cityService.GetAllCity());
        }
        [HttpGet("{cityId}")]
        public async Task<ActionResult<ServiceRepsonse<City>>> GetCityById(int cityId)
        {
            return Ok(await _cityService.GetCity(cityId));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceRepsonse<int>>> InsertCity(AddCityDto city)
        {
            return Ok(await _cityService.AddCity(city));
        }

        [HttpPut("{cityId}")]
        public async Task<ActionResult<ServiceRepsonse<List<GetCityDto>>>> UpdateCity(UpdateCityDto updateCityDto, int cityId)
        {
            var response = await _cityService.UpdateCity(updateCityDto, cityId);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
        [HttpDelete("{cityId}")]
        public async Task<ActionResult<HttpStatusCode>> DeleteCity(int cityId)
        {
            var response = _cityService.DeleteCity(cityId);

            return Ok(response);
        }


        /*public void UpdateCityAddMeasurement( Measurement measurement)
        {
            var entity = weatherdbContext.City.FirstOrDefault(
                c => c.CityId == measurement.CityId);
            entity.Measurement.Add(measurement);
           weatherdbContext.SaveChangesAsync();
           
        }*/

    }
}
