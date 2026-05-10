using VehicleGuard.Shared.DTOs.Gps;

namespace VehicleGuard.Shared.Interfaces.Gps;

public interface IGpsRepository
{
    Task<GpsDto?> CreateAsync(GpsDto gpsDto, int userId);
    Task<List<GpsDto>?> GetAllByDeviceAsync(int embeddedDeviceId, int userId);
    Task<GpsDto?> GetByIdAsync(int embeddedDeviceId, int gpsId, int userId);
}