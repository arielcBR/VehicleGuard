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
            .Property(x => x.EmbeddedDeviceId)
            .HasColumnName("EmbeddedDeviceId")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(x => x.VehicleId)
            .HasColumnName("VehicleId")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(x => x.Latitude)
            .HasColumnName("Latitude")
            .HasColumnType("float")
            .IsRequired();

        builder
            .Property(x => x.Longitude)
            .HasColumnName("Longitude")
            .HasColumnType("float")
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
            .Property(x => x.IsUserNearby)
            .HasColumnName("IsUserNearby")
            .HasColumnType("BIT")
            .IsRequired();

        builder
            .Property(x => x.OccurredAt)
            .HasColumnName("OccurredAt")
            .HasColumnType("datetime2")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("getdate()");

        // Relationships
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.VehicleEvents)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_VehicleEvents_User_UserId")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .HasConstraintName("FK_VehicleEvents_Vehicle_VehicleId")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.EmbeddedDevice)
            .WithMany()
            .HasForeignKey(x => x.EmbeddedDeviceId)
            .HasConstraintName("FK_VehicleEvents_EmbeddedDevice_EmbeddedDeviceId")
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("IX_VehicleEvents_UserId");

        builder
            .HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_VehicleEvents_VehicleId");

        builder
            .HasIndex(x => x.EmbeddedDeviceId)
            .HasDatabaseName("IX_VehicleEvents_EmbeddedDeviceId");

        builder
            .HasIndex(x => x.OccurredAt)
            .HasDatabaseName("IX_VehicleEvents_OccurredAt");
    }
}