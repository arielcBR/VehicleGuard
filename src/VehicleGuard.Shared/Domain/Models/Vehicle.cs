namespace VehicleGuard.Shared.Domain.Models;

public class Vehicle
{
    public int Id { get; set; }
    public required string LicensePlate { get; set; }
    public required string Color { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public int EmbeddedDeviceId { get; set; }
    public EmbeddedDevice EmbeddedDevice { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public IList<CommandLog> CommandLogs { get; set; } = new List<CommandLog>();
}