using Microsoft.EntityFrameworkCore;
using VehicleGuard.Shared.Infrastructure.Data;
using VehicleGuard.Shared.DTOs.Vehicles;
using VehicleGuard.Shared.Interfaces.Vehicle;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Shared.Repositories.Vehicles;

public class VehicleRepository : IVehicleRepository
{
    private readonly TrackerDbContext _db;

    public VehicleRepository(TrackerDbContext db)
    {
        _db = db;
    }
    
    public async Task<Vehicle?> CreateAsync(CreateVehicleDto vehicleDto, int userId)
    {
        var vehicleExists = await this.GetByLicensePlateAsync(vehicleDto.LicensePlate);

        if (vehicleExists != null)
            return null;
        
        var vehicle = new Vehicle
        {
            LicensePlate = vehicleDto.LicensePlate.ToLower(),
            Color = vehicleDto.Color.ToLower(),
            Brand = vehicleDto.Brand.ToLower(),
            Model = vehicleDto.Model.ToLower(),
            UserId = userId
        };
        
        var result = await _db.Vehicles.AddAsync(vehicle);
        await _db.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<List<VehicleDto>> GetAllAsync(int userId)
    {
        var vehicles = await _db.Vehicles
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();

        if (vehicles.Count == 0)
            return new List<VehicleDto>();
        
        List<VehicleDto> vehicleDtos = new List<VehicleDto>();
        foreach (var vehicle in vehicles)
        {
            VehicleDto vehicleDto = new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Color = vehicle.Color,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
            };
            vehicleDtos.Add(vehicleDto);
        }
        
        return vehicleDtos;
    }
    
    public async Task<Vehicle?> GetModelByLicensePlateAsync(string licensePlate)
        =>  await _db.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);

    public async Task<VehicleDto?> GetByLicensePlateAsync(string licensePlate)
    {
        var vehicle = await _db.Vehicles.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
        
        if(vehicle == null)
            return null;
        
        return new VehicleDto()
        {
            LicensePlate = vehicle.LicensePlate,
            Color = vehicle.Color,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
        };
    }

    public async Task<Vehicle?> GetByIdAsync(int vehicleId, int userId)
        => await _db.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == vehicleId && x.UserId == userId);

    public async Task<bool> DeleteAsync(int vehicleId, int userId)
    {
        var vehicle = await this.GetByIdAsync(vehicleId, userId);
        if (vehicle == null)
            return false;
        
        _db.Vehicles.Remove(vehicle);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<VehicleDto?> UpdateAsync(UpdateVehicleDto updateVehicleDto, int vehicleId, int userId)
    {
        var vehicleInDatabase = await _db.Vehicles.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == vehicleId);

        if (vehicleInDatabase == null)
            return null;

        if (!string.IsNullOrEmpty(updateVehicleDto.Color))
        {
            vehicleInDatabase.Color = updateVehicleDto.Color;
            vehicleInDatabase.UpdatedAt = DateTime.Now;
        }

        _db.Vehicles.Update(vehicleInDatabase);
        await _db.SaveChangesAsync();

        return new VehicleDto
        {
            Id = vehicleInDatabase.Id,
            LicensePlate = vehicleInDatabase.LicensePlate,
            Color = vehicleInDatabase.Color,
            Brand = vehicleInDatabase.Brand,
            Model = vehicleInDatabase.Model,
        };
    }

}