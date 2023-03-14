using AutoMapper;

using Microsoft.EntityFrameworkCore;

using weatherforecast.Data;
using weatherforecast.Dto;
using weatherforecast.Models;

public class MeasurementService : IMeasurementService
{

    private readonly IMapper _mapper;
    public WeatherDataContext _context;

    public MeasurementService(IMapper mapper, WeatherDataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceRepsonse<List<GetMeasurementDto>>> AddMeasurement(AddMeasurementDto newMeasurement)
    {
        var serviceResponse = new ServiceRepsonse<List<GetMeasurementDto>>();
        var measurement = _mapper.Map<Measurement>(newMeasurement);

        measurement.CityId = newMeasurement.CityId;
        _context.Measurement.Add(measurement);
        await _context.SaveChangesAsync();
        serviceResponse.Data = await _context.Measurement
                                    .Select(c => _mapper.Map<GetMeasurementDto>(c))
                                    .ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceRepsonse<string>> DeleteMeasurement(int measurementId)
    {
        var serviceResponse = new ServiceRepsonse<string>();
        var measurement = await _context.Measurement
                                        .FirstOrDefaultAsync(m => m.MesurementId == measurementId);

        if (measurement == null)
        {
            throw new Exception($"Measurement with id = {measurementId} not found.");
        }

        _context.Measurement.Remove(measurement);
        await _context.SaveChangesAsync();
        serviceResponse.Data = $"Measurement with id = {measurementId} is deleted.";
        return serviceResponse;

    }

    public async Task<ServiceRepsonse<List<GetMeasurementDto>>> GetAllMeasurements()
    {
        var serviceResponse = new ServiceRepsonse<List<GetMeasurementDto>>();
        var measurements = await _context.Measurement.ToListAsync();
        serviceResponse.Data = measurements
                                    .Select(m => _mapper.Map<GetMeasurementDto>(m))
                                    .ToList();
        return serviceResponse;
    }

    public async Task<ServiceRepsonse<GetMeasurementDto>> GetMeasurementById(int id)
    {
        var serviceResponse = new ServiceRepsonse<GetMeasurementDto>();
        var measurement = await _context.Measurement.FirstOrDefaultAsync(m => m.MesurementId == id);
        serviceResponse.Data = _mapper.Map<GetMeasurementDto>(measurement);
        return serviceResponse;
    }

    public async Task<ServiceRepsonse<GetMeasurementDto>> UpdateMeasurement(UpdateMeasurementDto updateMeasurementDto)
    {
        var serviceResponse = new ServiceRepsonse<GetMeasurementDto>();

        try
        {
            var measurement = await _context.Measurement
                                            .FirstOrDefaultAsync(m => m.MesurementId == updateMeasurementDto.MesurementId);

            if (measurement is null)
            {
                throw new Exception($"Measurement with id = {updateMeasurementDto} not found.");
            }

            measurement.Humdity = updateMeasurementDto.Humdity;
            measurement.WindSpeed = updateMeasurementDto.WindSpeed;
            measurement.MaxTemp = updateMeasurementDto.MaxTemp;
            measurement.MinTemp = updateMeasurementDto.MinTemp;

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetMeasurementDto>(measurement);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}