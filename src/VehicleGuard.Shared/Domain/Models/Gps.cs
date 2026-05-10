namespace VehicleGuard.Shared.Domain.Models;

public class Gps
{
    public int Id { get; set; }
    public int Age { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Hdop { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int EmbeddedDeviceId { get; set; }
    public EmbeddedDevice EmbeddedDevice { get; set; } = null!;
}