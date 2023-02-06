using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using weatherforecast.Entities;

namespace weatherforecast.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CityController : ControllerBase
    {
        private readonly WeatherdbContext weatherdbContext;
        public CityController(WeatherdbContext weatherdbContext)
        {
            this.weatherdbContext = weatherdbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> Get()
        {
            var List = await weatherdbContext.Cities.Select(
                c=> new City { CityName = c.CityName, CityId = c.CityId 
                }
                ).ToListAsync(); 
            if(List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
        [HttpGet("{cityId}")]
        public async Task<ActionResult<City>> GetCityById(int cityId)
        {
            City City = await weatherdbContext.Cities
                .Select(
                    c=> new City {
                        CityId=c.CityId,
                         CityName= c.CityName
                    }).FirstOrDefaultAsync(c => c.CityId == cityId);
            if (City == null)
            {
                return NotFound();
            } else
            {
                return City; 
            }
          
        }
        [HttpPost]
        public async Task<HttpStatusCode> InsertCity(City city)
        {
            var entity = new City { CityName = city.CityName, CityId = city.CityId,  };
            weatherdbContext.Cities.Add(entity);
            weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("{CityId}")]
        public async Task<HttpStatusCode> UpdateCity(City city)
        {
            var entity = await weatherdbContext.Cities.FirstOrDefaultAsync(
                c => c.CityId == city.CityId);
            entity.CityName=city.CityName;
            await weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpDelete("{cityId}")]
        public async Task<HttpStatusCode> DeleteCity(int cityId)
        {
            var entity = new City { CityId = cityId };
            weatherdbContext.Cities.Attach(entity);
            weatherdbContext.Cities.Remove(entity);
            await weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }


        /*public void UpdateCityAddMeasurement( Measurement measurement)
        {
            var entity = weatherdbContext.Cities.FirstOrDefault(
                c => c.CityId == measurement.CityId);
            entity.Measurements.Add(measurement);
           weatherdbContext.SaveChangesAsync();
           
        }*/

    }
}
