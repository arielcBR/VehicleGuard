using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Users;

public record UpdateUserDto(
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    string? UserName,

    [EmailAddress(ErrorMessage = "Email is not valid")]
    string? Email
);