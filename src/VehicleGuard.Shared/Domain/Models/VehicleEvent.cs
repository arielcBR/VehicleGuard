using VehicleGuard.Shared.Domain.Enums;

namespace VehicleGuard.Shared.Domain.Models;

public class VehicleEvent
{
    public int Id { get; set; }
    public int GpsId { get; set; }
    public int EmbeddedDeviceId { get; set; }  
    public VehicleEventClassification Classification { get; set; }
    public bool IsSensitivePeriod { get; set; }
    public DateTime OccurredAt { get; set; }

    public Gps Gps { get; set; } = null!;
    public EmbeddedDevice EmbeddedDevice { get; set; } = null!;
}