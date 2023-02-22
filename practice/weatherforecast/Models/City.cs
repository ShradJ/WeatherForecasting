using System.ComponentModel.DataAnnotations.Schema;

using weatherforecast.Dto;

namespace weatherforecast.Models;

public class City
{
    [Column("city_id")]
    public int CityId { get; set; }
    [Column("city_name")]
    public string CityName { get; set; } = null!;

    public List<GetMeasurementDto>? Measurements { get; set; }
}
