using System.ComponentModel.DataAnnotations;

namespace weatherforecast.Dto
{
    public class GetMeasurementDto
    {
        [Key]
        public int MesurementId { get; set; }

        public string? MinTemp { get; set; }

        public string? MaxTemp { get; set; }

        public int? Humdity { get; set; }

        public int? WindSpeed { get; set; }

        public int CityId;
    }
}