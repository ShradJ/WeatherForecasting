using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using weatherforecast.Entities;

namespace weatherforecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly WeatherdbContext weatherdbContext;

        public MeasurementController(WeatherdbContext weatherdbContext)
        {
            this.weatherdbContext = weatherdbContext;
        }
        [HttpGet("measurement")]
        public async Task<ActionResult<List<Measurement>>> Get()
        {
            var List = await weatherdbContext.Measurements.Select(
                m => new Measurement
                {
                    MesurementId = m.MesurementId,
                    CityId = m.CityId,
                    MinTemp = m.MinTemp,
                    MaxTemp = m.MaxTemp,
                    Timestamp = m.Timestamp,
                    Humdity = m.Humdity,
                    WindSpeed = m.WindSpeed
                }).ToListAsync();
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("MeasurementById")]
        public async Task<ActionResult<Measurement>> GetMeasurementById(int id)
        {
            Measurement measurement = await weatherdbContext.Measurements
                .Select(
                m => new Measurement
                {
                    MesurementId = m.MesurementId,
                    CityId = m.CityId,
                    MinTemp = m.MinTemp,
                    MaxTemp = m.MaxTemp,
                    Timestamp = m.Timestamp,
                    Humdity = m.Humdity,
                    WindSpeed = m.WindSpeed
                }).FirstOrDefaultAsync(m => m.MesurementId == id);

            if (measurement == null)
            {
                return NotFound();
            }
            else
            {
                return measurement;
            }
        }
        [HttpPost("InsertMeasurement")]
        public async Task<HttpStatusCode> InsertMeasurement(Measurement measurement)
        {
            var city = await weatherdbContext.Cities
                .FirstOrDefaultAsync(c => c.CityId == measurement.CityId);
           // measurement.City = city;
            var entity = new Measurement
            {
                MesurementId = measurement.MesurementId,
                CityId = measurement.CityId,
                MinTemp = measurement.MinTemp,
                MaxTemp = measurement.MaxTemp,
                Timestamp = measurement.Timestamp,
                Humdity = measurement.Humdity,
                WindSpeed = measurement.WindSpeed,
               // City = measurement.City
            };
             //city.Measurements.Add(entity);
            weatherdbContext.Measurements.Add(entity);
             await weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

       
    }
}
