using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Gps;

public record GpsDto(
    [Required(ErrorMessage = "Age is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Age is out of range")]
    int Age,

    [Required(ErrorMessage = "Latitude is required")]
    double Latitude,

    [Required(ErrorMessage = "Longitude is required")]
    double Longitude,

    [Required(ErrorMessage = "Hdop is required")]
    double Hdop,

    [Required(ErrorMessage = "Embedded Device Id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Embedded Device Id is invalid")]
    int EmbeddedDeviceId
);