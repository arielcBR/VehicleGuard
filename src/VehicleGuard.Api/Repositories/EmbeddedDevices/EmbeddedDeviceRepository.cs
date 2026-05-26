using Microsoft.EntityFrameworkCore;
using VehicleGuard.Api.Infrastructure.Data;
using VehicleGuard.Shared.DTOs.EmbeddedDevices;
using VehicleGuard.Shared.Interfaces.EmbeddedDevices;
using VehicleGuard.Shared.Interfaces.Vehicle;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Repositories.EmbeddedDevices;

public class EmbeddedDeviceRepository : IEmbeddedDeviceRepository
{
    private readonly VehicleGuardDbContext _db;
    private readonly IVehicleRepository _vehicleRepository;

    public EmbeddedDeviceRepository(VehicleGuardDbContext database, IVehicleRepository vehicleRepository)
    {
        _db = database;
        _vehicleRepository = vehicleRepository;
    }
    
    public async Task<EmbeddedDeviceDto?> CreateAsync(CreateEmbeddedDeviceDto deviceEmbeddedDto, int userId)
    {
        var vehicleId = deviceEmbeddedDto.VehicleId;
        var vehicleInDatabase = await _vehicleRepository.GetByIdAsync(vehicleId, userId);

        if (vehicleInDatabase == null)
            return null;

        var embeddedDevice = new EmbeddedDevice
        {
            VehicleId = vehicleId,
            CreatedAt = DateTime.UtcNow,
        };

        var embeddedDeviceCreated = await _db.EmbeddedDevices.AddAsync(embeddedDevice);
        await _db.SaveChangesAsync();

        var result = new EmbeddedDeviceDto
        (
            Id: embeddedDeviceCreated.Entity.Id,
            VehicleId: embeddedDeviceCreated.Entity.VehicleId
        );
        
        return result;
    }

    public async Task<List<EmbeddedDeviceDto>> GetAllAsync(int userId)
    {
        var listOfEmbeddedDevice = await _db.EmbeddedDevices
            .Include(x => x.Vehicle)
            .AsNoTracking()
            .Where(x => x.Vehicle.UserId == userId)
            .Select(embeddedDevice => new EmbeddedDeviceDto 
            (
                Id: embeddedDevice.Id,
                VehicleId: embeddedDevice.VehicleId,
                UpdatedAt: embeddedDevice.UpdatedAt
            ))
            .ToListAsync();
        
        return listOfEmbeddedDevice.Count == 0 ? new List<EmbeddedDeviceDto>() : listOfEmbeddedDevice;
    }
    
    public async Task<EmbeddedDeviceDto?> GetByIdAsync(int embeddedDeviceId, int userId)
    {
        var embeddedDevice = await _db.EmbeddedDevices
            .Include(x => x.Vehicle) 
            .AsNoTracking() 
            .Where(x => x.Id == embeddedDeviceId && x.Vehicle.UserId == userId) 
            .Select(embeddedDevice => new EmbeddedDeviceDto(
                Id: embeddedDevice.Id,
                VehicleId: embeddedDevice.VehicleId,
                UpdatedAt: embeddedDevice.UpdatedAt
            ))
            .FirstOrDefaultAsync();
        
        if (embeddedDevice == null)
            return null;
        
        var lastGps = await _db.Gps
            .AsNoTracking()
            .Where(x => x.EmbeddedDeviceId == embeddedDeviceId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
        
        if (lastGps == null)
            return embeddedDevice;
        
        return embeddedDevice with
        {
            Latitude = lastGps.Latitude,
            Longitude = lastGps.Longitude,
            Hdop = lastGps.Hdop,
            Age = lastGps.Age,
            LastSeenAt = DateTime.UtcNow - lastGps.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int embeddedDeviceId, int userId)
    {
        var embeddedDevice = await _db.EmbeddedDevices
            .Include(x => x.Vehicle)
            .AsNoTracking()
            .Where(x => x.Id == embeddedDeviceId && x.Vehicle.UserId == userId)
            .FirstOrDefaultAsync();

        if (embeddedDevice == null)
            return false;
        
        _db.EmbeddedDevices.Remove(embeddedDevice);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasDeviceInstalledAsync(int vehicleId)
        => await _db.EmbeddedDevices.AnyAsync(x => x.VehicleId == vehicleId);
        
}