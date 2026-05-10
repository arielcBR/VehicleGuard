namespace VehicleGuard.Shared.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}