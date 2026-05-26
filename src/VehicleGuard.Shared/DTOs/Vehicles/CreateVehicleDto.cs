using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Vehicles;

public record CreateVehicleDto(
    [Required(ErrorMessage = "License plate is required")]
    [MaxLength(7, ErrorMessage = "The license plate is too long")]
    string LicensePlate,

    [Required(ErrorMessage = "Color is required")]
    string Color,

    [Required(ErrorMessage = "Brand is required")]
    string Brand,

    [Required(ErrorMessage = "Model is required")]
    string Model
);