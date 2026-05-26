using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Users;

public record CreateUserDto(
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    string Name,

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters")]
    [MaxLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
    string Password,

    [Required(ErrorMessage = "Repeat password is required")]
    string RepeatPassword
);