using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Shared.Infrastructure.Data.Mapping;

public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        builder
            .Property(x => x.LicensePlate)
            .HasColumnName("LicensePlate")
            .HasColumnType("NVARCHAR(60)")
            .IsRequired();
        builder
            .Property(x => x.Color)
            .HasColumnName("Color")
            .HasColumnType("NVARCHAR(30)")
            .IsRequired();
        builder
            .Property(x => x.Brand)
            .HasColumnName("Brand")
            .HasColumnType("NVARCHAR(60)")
            .IsRequired();
        builder
            .Property(x => x.Model)
            .HasColumnName("Model")
            .HasColumnType("NVARCHAR(60)")
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
            .HasOne(x => x.User)
            .WithMany(x => x.Vehicles)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_Vehicles_User")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.EmbeddedDevice)
            .WithOne(x => x.Vehicle)
            .HasForeignKey<EmbeddedDevice>(x => x.VehicleId)
            .HasConstraintName("FK_Vehicles_EmbeddedDevice")
            .OnDelete(DeleteBehavior.Cascade);
        
        // Indexes
        builder
            .HasIndex(x => x.LicensePlate)
            .HasDatabaseName("IX_Vehicle_LicensePlate")
            .IsUnique();

    }
}