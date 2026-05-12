using VehicleGuard.Shared.Domain.Enums;

namespace VehicleGuard.Shared.Domain.Models;

public class VehicleEvent
{
    public int Id { get; set; }
    public int EmbeddedDeviceId { get; set; }
    public int VehicleId { get; set; }
    public int UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public VehicleEventClassification Classification { get; set; }
    public bool IsSensitivePeriod { get; set; }
    public bool IsUserNearby { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public EmbeddedDevice EmbeddedDevice { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
    public User User { get; set; } = null!;
}