using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Users;

public class CreateUserDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters")]
    [MaxLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
    // Adicionar Regex para padrão de senha
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Repeat password is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string RepeatPassword { get; set; } = string.Empty;
}