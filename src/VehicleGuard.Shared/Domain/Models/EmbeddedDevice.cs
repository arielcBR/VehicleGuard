namespace VehicleGuard.Shared.Domain.Models;

public class EmbeddedDevice
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public IList<CommandLog> CommandLogs { get; set; } = new List<CommandLog>();
    public IList<Gps> Gps { get; set; } = new List<Gps>();
    public IList<VehicleEvent> VehicleEvents { get; set; } = new List<VehicleEvent>();
    
}