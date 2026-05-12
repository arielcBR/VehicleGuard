using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Infrastructure.Data.Mapping;

public class NotificationLogMap : IEntityTypeConfiguration<NotificationLog>
{
    public void Configure(EntityTypeBuilder<NotificationLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(x => x.VehicleEventId)
            .HasColumnName("VehicleEventId")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder
            .Property(x => x.Body)
            .HasColumnName("Body")
            .HasColumnType("NVARCHAR(500)")
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasConversion<string>()
            .HasColumnName("Status")
            .HasColumnType("NVARCHAR(30)")
            .IsRequired();

        builder
            .Property(x => x.SentAt)
            .HasColumnName("SentAt")
            .HasColumnType("datetime2")
            .IsRequired(false);

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("getdate()");

        // Relationships
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.NotificationLogs)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_NotificationLogs_User_UserId")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.VehicleEvent)
            .WithMany()
            .HasForeignKey(x => x.VehicleEventId)
            .HasConstraintName("FK_NotificationLogs_VehicleEvent_VehicleEventId")
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("IX_NotificationLogs_UserId");

        builder
            .HasIndex(x => x.VehicleEventId)
            .HasDatabaseName("IX_NotificationLogs_VehicleEventId");
    }
}