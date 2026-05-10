using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.Gps;

public class GpsDto
{
    [Required(ErrorMessage = "Age is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Age is out of range")]
    public int Age { get; set; }
    
    [Required(ErrorMessage = "Latitude is required")]
    public double Latitude { get; set; }
    
    [Required(ErrorMessage = "Longitude is required")]
    public double Longitude { get; set; }
    
    [Required(ErrorMessage = "Hdop is required")]
    public double Hdop { get; set; }
    
    [Required(ErrorMessage = "Embedded Device Id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Embedded Device Id is invalid")]
    public int EmbeddedDeviceId { get; set; } 
}