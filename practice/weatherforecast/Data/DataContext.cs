using Microsoft.EntityFrameworkCore;

using weatherforecast.Models;

namespace weatherforecast.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySQL(connectionString);
        }

        public DbSet<Measurement> Measurement { get; set; }
        public DbSet<City> City { get; set; }
    }
}
