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
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySQL(connectionString);
        }




        /*public DataContext(DbContextOptions<DbContext> options) :base(options)
        {

        }*/
        public DbSet<Measurement> Measurement { get; set; }
        public DbSet<City> City { get; set; }
    }
}
