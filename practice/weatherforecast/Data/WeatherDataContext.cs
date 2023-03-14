using Microsoft.EntityFrameworkCore;

using weatherforecast.Models;

namespace weatherforecast.Data
{
    public class WeatherDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public WeatherDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public WeatherDataContext()
        {
            Configuration = null;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySQL(connectionString);
        }

        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<City> City { get; set; }
    }
}
