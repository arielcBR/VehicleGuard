using Microsoft.EntityFrameworkCore;
using VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Shared.Infrastructure.Data.Mapping;

namespace VehicleGuard.Shared.Infrastructure.Data;

public class TrackerDbContext : DbContext
{
    public TrackerDbContext(DbContextOptions<TrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<EmbeddedDevice> EmbeddedDevices { get; set; }
    public DbSet<CommandLog> CommandLogs { get; set; }
    public DbSet<Gps> Gps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CommandLogMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new EmbeddedDeviceMap());
        modelBuilder.ApplyConfiguration(new GpsMap());
    }
}