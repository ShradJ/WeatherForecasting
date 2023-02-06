using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace weatherforecast.Entities;

public partial class WeatherdbContext : DbContext
{
    public WeatherdbContext()
    {
    }

    public WeatherdbContext(DbContextOptions<WeatherdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PRIMARY");

            entity.ToTable("city");

            entity.HasIndex(e => e.CityName, "city_name_UNIQUE").IsUnique();

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(45)
                .HasColumnName("city_name");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => new { e.MesurementId, e.CityId }).HasName("PRIMARY");

            entity.ToTable("measurement");

            entity.HasIndex(e => e.CityId, "fk_measurement_city_idx");

            entity.Property(e => e.MesurementId)
                .ValueGeneratedOnAdd()
                .HasColumnName("mesurement_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Humdity).HasColumnName("humdity");
            entity.Property(e => e.MaxTemp)
                .HasMaxLength(45)
                .HasColumnName("max_temp");
            entity.Property(e => e.MinTemp)
                .HasMaxLength(45)
                .HasColumnName("min_temp");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");

            entity.HasOne(d => d.City).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_measurement_city");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
