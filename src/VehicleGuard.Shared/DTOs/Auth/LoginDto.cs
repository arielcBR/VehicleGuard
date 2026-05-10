using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters")]
    [MaxLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
    public string Password { get; set; } = string.Empty;
}