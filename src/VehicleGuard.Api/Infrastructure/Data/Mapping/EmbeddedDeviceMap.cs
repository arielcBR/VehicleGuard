using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;
namespace VehicleGuard.Api.Infrastructure.Data.Mapping;

public class EmbeddedDeviceMap : IEntityTypeConfiguration<EmbeddedDevice>
{
    public void Configure(EntityTypeBuilder<EmbeddedDevice> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.VehicleId)
            .HasColumnName("VehicleId")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("getdate()");
        
        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime2")
            .IsRequired(false);
        
        // Relationships
        builder
            .HasOne(x => x.Vehicle)
            .WithOne(x => x.EmbeddedDevice)
            .HasForeignKey<EmbeddedDevice>(x => x.VehicleId)
            .HasConstraintName("FK_EmbeddedDevice_Vehicle_VehicleId");
        
        builder
            .HasMany(x => x.Gps)
            .WithOne(x => x.EmbeddedDevice)
            .HasForeignKey(x => x.EmbeddedDeviceId)
            .HasConstraintName("FK_Gps_EmbeddedDevice_EmbeddedDeviceId");

        builder
            .HasMany(x => x.VehicleEvents)
            .WithOne(x => x.EmbeddedDevice)
            .HasForeignKey(x => x.EmbeddedDeviceId)
            .HasConstraintName("FK_VehicleEvents_EmbeddedDevice_EmbeddedDeviceId")
            .OnDelete(DeleteBehavior.Restrict);
        
        // Indexes
        builder
            .HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_EmbeddedDevice_VehicleId")
            .IsUnique();
    }
}