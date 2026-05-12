using VehicleGuard.Shared.Domain.Enums;

namespace VehicleGuard.Shared.Domain.Models;

public class NotificationLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VehicleEventId { get; set; }
    public string Title  { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public NotificationLogStatus Status  { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public User User { get; set; } = null!;
    public VehicleEvent VehicleEvent { get; set; } = null!;
}