using Microsoft.EntityFrameworkCore;
using VehicleGuard.Api.Infrastructure.Data;
using VehicleGuard.Shared.DTOs.Gps;
using VehicleGuard.Shared.Interfaces.Gps;
using Entity = VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Api.Repositories.Gps;

public class GpsRepository : IGpsRepository
{
    private readonly VehicleGuardDbContext _db;

    public GpsRepository(VehicleGuardDbContext db)
    {
        _db = db;
    }
    
    public async Task<GpsDto?> CreateAsync(GpsDto gpsDto, int userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            return null;

        var embeddedDevice = await _db.EmbeddedDevices.FirstOrDefaultAsync(x => x.Id == gpsDto.EmbeddedDeviceId);
        if (embeddedDevice == null)
            return null;

        var gps = new Entity.Gps
        {
            Age = gpsDto.Age,
            Latitude = gpsDto.Latitude,
            Longitude = gpsDto.Longitude,
            Hdop = gpsDto.Hdop,
            CreatedAt = DateTime.UtcNow,
            EmbeddedDeviceId =  embeddedDevice.Id,
        }; 
        
        var result = await _db.Gps.AddAsync(gps);
        await _db.SaveChangesAsync();

        return new GpsDto
        {
            Age = result.Entity.Age,
            Latitude = result.Entity.Latitude,
            Longitude = result.Entity.Longitude,
            Hdop = result.Entity.Hdop,
            EmbeddedDeviceId = result.Entity.EmbeddedDeviceId,
        };
    }

    public async Task<List<GpsDto>?> GetAllByDeviceAsync(int embeddedDeviceId, int userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            return null;

        var embeddedDevice = await _db.EmbeddedDevices.FirstOrDefaultAsync(x => x.Id == embeddedDeviceId);
        if (embeddedDevice == null)
            return null;

        var listOfGps = await _db.Gps
            .AsNoTracking()
            .Where(x => x.EmbeddedDeviceId == embeddedDeviceId)
            .Select(gps => new GpsDto
            {
                Age = gps.Age,
                Latitude = gps.Latitude,
                Longitude = gps.Longitude,
                Hdop = gps.Hdop,
                EmbeddedDeviceId = gps.EmbeddedDeviceId,
            })
            .ToListAsync();
        
        return listOfGps;
    }

    public async Task<GpsDto?> GetByIdAsync(int embeddedDeviceId, int gpsId, int userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            return null;

        var embeddedDevice = await _db.EmbeddedDevices.FirstOrDefaultAsync(x => x.Id == embeddedDeviceId);
        if (embeddedDevice == null)
            return null;

        var gps = await _db.Gps
            .AsNoTracking()
            .Where(x => x.Id == gpsId && x.EmbeddedDeviceId == embeddedDeviceId)
            .Select(gps => new GpsDto
            {
                Age = gps.Age,
                Latitude = gps.Latitude,
                Longitude = gps.Longitude,
                Hdop = gps.Hdop,
                EmbeddedDeviceId = gps.EmbeddedDeviceId,
            })
            .FirstOrDefaultAsync();
        
        return gps;
    }
}