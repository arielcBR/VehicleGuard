namespace VehicleGuard.Shared.DTOs.EmbeddedDevices;

public record EmbeddedDeviceDto(
    int Id,
    int VehicleId,
    DateTime? UpdatedAt = null,
    int? Age = null,
    double? Latitude = null,
    double? Longitude = null,
    double? Hdop = null,
    TimeSpan? LastSeenAt = null
);
