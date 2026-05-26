using VehicleGuard.Shared.DTOs.EmbeddedDevices;

namespace VehicleGuard.Shared.Interfaces.EmbeddedDevices;

public interface IEmbeddedDeviceRepository
{
    Task<EmbeddedDeviceDto?> CreateAsync(CreateEmbeddedDeviceDto deviceEmbeddedDto, int userId);
    Task<EmbeddedDeviceDto?> GetByIdAsync(int embeddedDeviceId, int userId);
    Task<List<EmbeddedDeviceDto>> GetAllAsync(int userId);
    Task<bool> HasDeviceInstalledAsync(int vehicleId);
    
    Task<bool> DeleteAsync(int embeddedDeviceId, int userId);

}