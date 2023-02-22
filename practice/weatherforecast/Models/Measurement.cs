using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weatherforecast.Models;

public partial class Measurement
{
    [Column("mesurement_id")]
    [Key]
    public int MesurementId { get; set; }


    /* [Column("timestamp")]
     public DateTime? Timestamp { get; set; }*/

    [Column("min_temp")]
    public string? MinTemp { get; set; }

    [Column("max_temp")]
    public string? MaxTemp { get; set; }

    [Column("humdity")]
    public int? Humdity { get; set; }

    [Column("wind_speed")]
    public int? WindSpeed { get; set; }


    [ForeignKey("city_id")]
    [Column("city_id")]
    public int CityId { get; set; }
}
