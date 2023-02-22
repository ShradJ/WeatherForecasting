using System.Net;

using Microsoft.AspNetCore.Mvc;

using weatherforecast.Dto;
using weatherforecast.Models;
namespace weatherforecast.Controllers
{
    [ApiController]
    [Route("measurements")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceRepsonse<List<GetMeasurementDto>>>> Get()
        {
            return Ok(await _measurementService.GetAllMeasurements());
        }
        [HttpGet("{measurementId}")]
        public async Task<ActionResult<ServiceRepsonse<GetMeasurementDto>>> GetMeasurementById(int measurementId)
        {
            return Ok(await _measurementService.GetMeasurementById(measurementId));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceRepsonse<List<GetMeasurementDto>>>> AddMeasurement(AddMeasurementDto addMeasurementDto)
        {
            return Ok(await _measurementService.AddMeasurement(addMeasurementDto));
        }
        [HttpPut("{measurementId}")]
        public async Task<ActionResult<ServiceRepsonse<GetMeasurementDto>>> UpdateMeasurement(UpdateMeasurementDto updateMeasurementDto)
        {
            var response = await _measurementService.UpdateMeasurement(updateMeasurementDto);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
        [HttpDelete("{measurementId}")]
        public async Task<ActionResult<HttpStatusCode>> DeleteMeasurement(int measurementId)
        {
            var response = _measurementService.DeleteMeasurement(measurementId);

            return Ok(response);
        }

    }
}
