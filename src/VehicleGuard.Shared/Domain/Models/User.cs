namespace VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Shared.Domain.Enums;
public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordHash { get; set; }
    public Role Role { get; set; }
    public string? FcmToken { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public IList<EmbeddedDevice> EmbeddedDevices { get; set; } = new List<EmbeddedDevice>();
    public IList<CommandLog> CommandLogs { get; set; } = new List<CommandLog>();
    public IList<VehicleEvent> VehicleEvents { get; set; } = new List<VehicleEvent>();
    public IList<NotificationLog> NotificationLogs { get; set; } = new List<NotificationLog>();
}