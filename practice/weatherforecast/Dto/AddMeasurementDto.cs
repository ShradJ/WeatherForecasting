namespace weatherforecast.Dto
{
    public class AddMeasurementDto
    {

        public string? MinTemp { get; set; }

        public string? MaxTemp { get; set; }

        public int? Humdity { get; set; }

        public int? WindSpeed { get; set; }
        public int CityId { get; set; }
    }
}