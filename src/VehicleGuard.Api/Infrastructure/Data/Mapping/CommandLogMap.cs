using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Infrastructure.Data.Mapping;

public class CommandLogMap : IEntityTypeConfiguration<CommandLog>
{
    public void Configure(EntityTypeBuilder<CommandLog> builder)
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
            .Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("INTEGER")
            .IsRequired();
        builder
            .Property(x => x.VehicleId)
            .HasColumnName("VehicleId")
            .HasColumnType("INTEGER")
            .IsRequired();
        builder
            .Property(x => x.Command)
            .HasConversion<string>()
            .HasColumnName("Command")
            .HasColumnType("NVARCHAR(60)")
            .IsRequired();
        builder
            .Property(x => x.Status)
            .HasConversion<string>()
            .HasColumnName("Status")
            .HasColumnType("NVARCHAR(60)")
            .IsRequired();
        builder
            .Property(x => x.RequestedAt)
            .HasColumnName("RequestedAt")
            .HasColumnType("datetime")
            .IsRequired(false);
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
            .WithMany(x => x.CommandLogs)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_CommandLogs_User_UserId")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(x => x.Vehicle)
            .WithMany(x => x.CommandLogs)
            .HasForeignKey(x => x.VehicleId)
            .HasConstraintName("FK_CommandLogs_Vehicle_VehicleId")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.EmbeddedDevice)
            .WithMany(x => x.CommandLogs)
            .HasForeignKey(x => x.EmbeddedDeviceId)
            .HasConstraintName("FK_CommandLogs_EmbeddedDevice_EmbeddedDeviceId")
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("IX_CommandLogs_UserId");
        builder
            .HasIndex(x => x.VehicleId)
            .HasDatabaseName("IX_CommandLogs_VehicleId");
        builder
            .HasIndex(x => x.EmbeddedDeviceId)
            .HasDatabaseName("IX_CommandLogs_EmbeddedDeviceId");
    }
}