using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Infrastructure.Data.Mapping;

public class VehicleEventMap : IEntityTypeConfiguration<VehicleEvent>
{
    public void Configure(EntityTypeBuilder<VehicleEvent> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        builder
            .Property(x => x.GpsId)
            .HasColumnName("GpsId")
            .HasColumnType("int")
            .IsRequired();
        
        builder
            .Property(x => x.EmbeddedDeviceId)
            .HasColumnName("EmbeddedDeviceId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Classification)
            .HasConversion<string>()
            .HasColumnName("Classification")
            .HasColumnType("NVARCHAR(30)")
            .IsRequired();

        builder
            .Property(x => x.IsSensitivePeriod)
            .HasColumnName("IsSensitivePeriod")
            .HasColumnType("BIT")
            .IsRequired();
        

        builder
            .Property(x => x.OccurredAt)
            .HasColumnName("OccurredAt")
            .HasColumnType("datetime2")
            .IsRequired();

        // Relationships
        builder
            .HasOne(x => x.Gps)
            .WithMany()
            .HasForeignKey(x => x.GpsId)
            .HasConstraintName("FK_VehicleEvents_Gps_GpsId")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(x => x.EmbeddedDevice)
            .WithMany(x => x.VehicleEvents)
            .HasForeignKey(x => x.EmbeddedDeviceId)
            .HasConstraintName("FK_VehicleEvents_EmbeddedDevice_EmbeddedDeviceId")
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes

        builder
            .HasIndex(x => x.OccurredAt)
            .HasDatabaseName("IX_VehicleEvents_OccurredAt");
    }
}