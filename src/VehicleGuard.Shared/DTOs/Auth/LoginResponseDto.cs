namespace VehicleGuard.Shared.DTOs.Auth;

public record LoginResponseDto(
    string Token, 
    DateTime Expiration
);