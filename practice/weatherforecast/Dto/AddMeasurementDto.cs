namespace weatherforecast.Dto
{
    public class AddMeasurementDto
    {
        //   public DateTime? Timestamp { get; set; }

        public string? MinTemp { get; set; }

        public string? MaxTemp { get; set; }

        public int? Humdity { get; set; }

        public int? WindSpeed { get; set; }
        public int CityId { get; set; }
        //public City City { get; set; }
    }
}