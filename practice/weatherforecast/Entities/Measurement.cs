using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace weatherforecast.Entities;
[Table("measurement")]
public partial class Measurement
{
    public int MesurementId { get; set; }

    public int CityId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? MinTemp { get; set; }

    public string? MaxTemp { get; set; }

    public int? Humdity { get; set; }

    public int? WindSpeed { get; set; }

   
    public virtual City City { get; set; } = null!;
}
