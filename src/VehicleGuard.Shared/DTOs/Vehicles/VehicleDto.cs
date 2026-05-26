namespace VehicleGuard.Shared.DTOs.Vehicles;

public record VehicleDto(
    int Id,
    string LicensePlate,
    string Brand,
    string Model,
    string Color
);