using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace weatherforecast.Entities;
[Table("city")]
public class City
{
    public City()
    {
        this.Measurements = new HashSet<Measurement>();
    }

    public int CityId { get; set; }

    public string CityName { get; set; } = null!;
    
    public virtual ICollection<Measurement> Measurements { get; } = new List<Measurement>();
}
