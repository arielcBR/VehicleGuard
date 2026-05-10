using VehicleGuard.Shared.DTOs.Users;
using VehicleGuard.Shared.Domain.Models;

namespace VehicleGuard.Shared.Interfaces.Users;

public interface IUserRepository
{
    Task<List<UserDto>?> Get();
    Task<UserDto> CreateAsync(User user);
    Task<UserDto?> GetByIdAsync(int id);
    Task<User?> GetEntityByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<UserDto?> UpdateAsync(int userId, UpdateUserDto userDto);
    Task<User?> DeleteAsync(int id);
}