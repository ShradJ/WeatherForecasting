using weatherforecast.Dto;
using weatherforecast.Models;

public interface IMeasurementService
{
    Task<ServiceRepsonse<List<GetMeasurementDto>>> GetAllMeasurements();
    Task<ServiceRepsonse<GetMeasurementDto>> GetMeasurementById(int id);
    Task<ServiceRepsonse<List<GetMeasurementDto>>> AddMeasurement(AddMeasurementDto measurement);
    Task<ServiceRepsonse<GetMeasurementDto>> UpdateMeasurement(UpdateMeasurementDto updateMeasurementDto);
    Task<ServiceRepsonse<string>> DeleteMeasurement(int measurementId);
}