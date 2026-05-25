using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Infrastructure.Data.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        builder
            .Property(x => x.Username)
            .HasColumnName("Username")
            .HasColumnType("NVARCHAR(60)")
            .IsRequired();
        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR(100)");
        builder.Ignore(x => x.Password);
        builder
            .Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR(100)");
        builder
            .Property(x => x.Role)
            .HasConversion<int>()
            .IsRequired();
        builder
            .Property(x => x.FcmToken)
            .HasColumnName("FcmToken")
            .HasColumnType("NVARCHAR(255)")
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
        

        builder
            .HasMany(x => x.NotificationLogs)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_NotificationLogs_User_UserId")
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder
            .HasIndex(x => x.Email)
            .HasDatabaseName("IX_User_Email")
            .IsUnique();
    }
}