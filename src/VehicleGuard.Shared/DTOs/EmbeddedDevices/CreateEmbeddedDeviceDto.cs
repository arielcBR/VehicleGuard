using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.EmbeddedDevices;

public class CreateEmbeddedDeviceDto
{
    [Required(ErrorMessage = "Vehicle Id is required")]
    public int VehicleId { get; set; }
}