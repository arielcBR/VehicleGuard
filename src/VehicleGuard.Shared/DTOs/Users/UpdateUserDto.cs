using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Users;

public class UpdateUserDto
{
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string? UserName { get; set; }
    
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string? Email { get; set; }
}
