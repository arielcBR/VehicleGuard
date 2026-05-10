namespace VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Shared.Domain.Enums;

public class CommandLog
{
    public int Id { get; set; }
    public int EmbeddedDeviceId { get; set; }
    public virtual EmbeddedDevice EmbeddedDevice { get; set; } = null!;
    
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    
    public Command Command { get; set; }
    public StatusCommand Status { get; set; }
    public DateTime? RequestedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}