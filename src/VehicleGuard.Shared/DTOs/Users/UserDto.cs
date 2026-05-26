using VehicleGuard.Shared.Domain.Enums;

namespace VehicleGuard.Shared.DTOs.Users;

public record UserDto(
    int Id,
    string UserName,
    Role Role,
    string Email,
    DateTime CreatedAt
);