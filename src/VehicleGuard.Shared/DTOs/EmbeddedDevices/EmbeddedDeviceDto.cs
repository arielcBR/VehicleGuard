namespace VehicleGuard.Shared.DTOs.EmbeddedDevices;

public class EmbeddedDeviceDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int? Age { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Hdop { get; set; }
    public TimeSpan? LastSeenAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}