using System.ComponentModel.DataAnnotations;

namespace VehicleGuard.Shared.DTOs.EmbeddedDevices;

public record CreateEmbeddedDeviceDto
(
    [Required(ErrorMessage = "Vehicle Id is required")]
    int VehicleId
);