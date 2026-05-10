using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Vehicles;

public class CreateVehicleDto
{
    [Required(ErrorMessage = "License plate is required")]
    [MaxLength(7, ErrorMessage = "The license plate is too long")]
    public required string LicensePlate { get; set; }
    
    [Required(ErrorMessage = "Color is required")]
    public required string Color { get; set; }
    
    [Required(ErrorMessage = "Brand is required")]
    public required string Brand { get; set; }
    
    [Required(ErrorMessage = "Model is required")]
    public required string Model { get; set; }
}