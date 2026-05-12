using Microsoft.EntityFrameworkCore;
using VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Api.Infrastructure.Data.Mapping;

namespace VehicleGuard.Api.Infrastructure.Data;

public class VehicleGuardDbContext : DbContext
{
    public VehicleGuardDbContext(DbContextOptions<VehicleGuardDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<EmbeddedDevice> EmbeddedDevices { get; set; }
    public DbSet<CommandLog> CommandLogs { get; set; }
    public DbSet<Gps> Gps { get; set; }
    public DbSet<VehicleEvent> VehicleEvents { get; set; }
    public DbSet<NotificationLog> NotificationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new EmbeddedDeviceMap());
        modelBuilder.ApplyConfiguration(new CommandLogMap());
        modelBuilder.ApplyConfiguration(new GpsMap());
        modelBuilder.ApplyConfiguration(new VehicleEventMap());
        modelBuilder.ApplyConfiguration(new NotificationLogMap());
    }
}