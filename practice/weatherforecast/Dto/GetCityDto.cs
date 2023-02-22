namespace weatherforecast.Dto
{
    public class GetCityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;

        public List<GetMeasurementDto>? Measurements { get; set; }
    }
}