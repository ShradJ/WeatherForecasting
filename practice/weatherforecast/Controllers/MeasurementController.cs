using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using weatherforecast.Entities;

namespace weatherforecast.Controllers
{
    [ApiController]
    [Route("cities")]
    public class MeasurementController : ControllerBase
    {
        private readonly WeatherdbContext weatherdbContext;

        public MeasurementController(WeatherdbContext weatherdbContext)
        {
            this.weatherdbContext = weatherdbContext;
        }
        [HttpGet("{cityId}/measurements")]
        public async Task<ActionResult<List<Measurement>>> Get(int cityId)
        {
            /*var List = await weatherdbContext.Measurements.Select(
                m => new Measurement
                {
                    MesurementId = m.MesurementId,
                    CityId = m.CityId,
                    MinTemp = m.MinTemp,
                    MaxTemp = m.MaxTemp,
                    Timestamp = m.Timestamp,
                    Humdity = m.Humdity,
                    WindSpeed = m.WindSpeed
                }).ToListAsync();*/
            var List = await weatherdbContext.Measurements.Where(m => m.CityId == cityId).ToListAsync();
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("{cityId}/measurements/{measurementId}")]
        public async Task<ActionResult<Measurement>> GetMeasurementById(int measurementId)
        {
            Measurement measurement = await weatherdbContext.Measurements
                                    .Where(m => m.MesurementId == measurementId)
                                    .FirstOrDefaultAsync();
                /*.Select(
                m => new Measurement
                {
                    MesurementId = m.MesurementId,
                    CityId = m.CityId,
                    MinTemp = m.MinTemp,
                    MaxTemp = m.MaxTemp,
                    Timestamp = m.Timestamp,
                    Humdity = m.Humdity,
                    WindSpeed = m.WindSpeed
                    
                })*/

            if (measurement == null)
            {
                return NotFound();
            }
            else
            {
                return measurement;
            }
        }
        [HttpPost("measurements")]
        public async Task<HttpStatusCode> InsertMeasurement(Measurement measurement)
        {
            var city = await weatherdbContext.Cities
                .FirstOrDefaultAsync(c => c.CityId == measurement.CityId);
           // measurement.City = city;
            /*var entity = new Measurement
            {
                MesurementId = measurement.MesurementId,
                CityId = measurement.CityId,
                MinTemp = measurement.MinTemp,
                MaxTemp = measurement.MaxTemp,
                Timestamp = measurement.Timestamp,
                Humdity = measurement.Humdity,
                WindSpeed = measurement.WindSpeed,
               // City = measurement.City
            };*/
             //city.Measurements.Add(entity);
         //   weatherdbContext.Measurements.Add(measurement);
         city.Measurements.Add(measurement);

            await weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("measurements/{measurementId}")]
        public async Task<HttpStatusCode> UpdateMeasurement(Measurement measurement, int measurementId)
        {
            var entity = await weatherdbContext.Measurements.FirstOrDefaultAsync(
                c => c.MesurementId == measurementId);
            var city = await weatherdbContext.Cities
               .FirstOrDefaultAsync(c => c.CityId == measurement.CityId);
            //city.Measurements.Remove(entity);
           // city.Measurements.Add(entity);
            entity.Humdity = measurement.Humdity;
            entity.Timestamp = measurement.Timestamp;
            entity.MinTemp = measurement.MinTemp;
            entity.MaxTemp = measurement.MaxTemp;
            
            await weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpDelete("measurements/{measurementId}")]
        public async Task<HttpStatusCode> DeleteMeasurement(int measurementId)
        {
            var entity = new Measurement { MesurementId = measurementId };
            weatherdbContext.Measurements.Attach(entity);
            weatherdbContext.Measurements.Remove(entity);
            await weatherdbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }


    }
}
