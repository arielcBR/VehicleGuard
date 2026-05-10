using VehicleGuard.Shared.DTOs.Vehicles;

namespace VehicleGuard.Shared.Interfaces.Vehicle;
using VehicleGuard.Shared.Domain.Models;

public interface IVehicleRepository
{
    Task<Vehicle?> CreateAsync(CreateVehicleDto vehicle, int id);
    Task<List<VehicleDto>> GetAllAsync(int userId);
    Task<Vehicle?> GetByIdAsync(int vehicleId, int userId);
    Task<VehicleDto?> GetByLicensePlateAsync(string licensePlate);

    Task<bool> DeleteAsync(int vehicleId, int userId);
    Task<Vehicle?> GetModelByLicensePlateAsync(string licensePlate);
    Task<VehicleDto?> UpdateAsync(UpdateVehicleDto vehicleUpdateDto, int vehicleId, int userId);
}