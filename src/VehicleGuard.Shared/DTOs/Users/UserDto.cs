using VehicleGuard.Shared.Domain.Enums;

namespace VehicleGuard.Shared.DTOs.Users;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public Role Role { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}