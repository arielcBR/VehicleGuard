using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Auth;

public record LoginDto(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters")]
    [MaxLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
    string Password
);