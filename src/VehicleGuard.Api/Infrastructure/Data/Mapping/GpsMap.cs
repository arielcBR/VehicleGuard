using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Infrastructure.Data.Mapping;

public class GpsMap : IEntityTypeConfiguration<Gps>
{
    public void Configure(EntityTypeBuilder<Gps> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        builder
            .Property(x => x.Age)
            .HasColumnName("Age")
            .HasColumnType("Int");
        
        builder
            .Property(x => x.Latitude)
            .HasColumnName("Latitude")
            .HasColumnType("float");

        builder
            .Property(x => x.Longitude)
            .HasColumnName("Longitude")
            .HasColumnType("float");
        
        builder
            .Property(x => x.Hdop)
            .HasColumnName("Hdop")
            .HasColumnType("float");
        
        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();
        
        // Foreign Keys
        builder
            .Property(x => x.EmbeddedDeviceId)
            .HasColumnName("EmbeddedDeviceId")
            .HasColumnType("Int")
            .IsRequired();
        
        // Index
        builder
            .HasIndex(x => x.EmbeddedDeviceId)
            .HasDatabaseName("IX_Gps_EmbeddedDeviceId");
    }
}