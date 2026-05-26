using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleGuard.Api.Infrastructure.Data;
using VehicleGuard.Shared.Domain.Enums;
using VehicleGuard.Shared.DTOs.Users;
using VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Shared.Interfaces.Users;

namespace VehicleGuard.Api.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly VehicleGuardDbContext _db;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRepository(VehicleGuardDbContext db, IPasswordHasher<User> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<UserDto>?> Get()
    {
        var result = await _db.Users
            .AsNoTracking()
            .Select(user => new UserDto
            (
                Id: user.Id,
                UserName: user.Username,
                Role: user.Role,
                Email: user.Email,
                CreatedAt: user.CreatedAt
            ))
            .ToListAsync();

        if (result.Count == 0)
            return new List<UserDto>();
        return result;

    }
    
    public async Task<UserDto> CreateAsync(User user)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user, user.Password);
        user.CreatedAt = DateTime.UtcNow;
        user.Role = Role.User;
        var result = await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        
        var userResponse = new UserDto
        (
            Id: result.Entity.Id,
            UserName: result.Entity.Username,
            Role: result.Entity.Role,
            Email: result.Entity.Email,
            CreatedAt: result.Entity.CreatedAt
        );
        return userResponse;
    }

    public async Task<UserDto?> GetByIdAsync(int id)  
        => await _db.Users
            .Select(user => new UserDto
            (
                Id: user.Id,
                UserName: user.Username,
                Role: user.Role,
                Email: user.Email,
                CreatedAt: user.CreatedAt
            ))
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    
    public async Task<User?> GetEntityByIdAsync(int id)  
        => await _db.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<User?> GetByEmailAsync(string email)
        => await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email);

    public async Task<UserDto?> UpdateAsync(int userId, UpdateUserDto userDto)
    {
        var userDatabase = await this.GetEntityByIdAsync(userId);
        
        if (userDatabase == null)
            return null;

        if (!string.IsNullOrWhiteSpace(userDto.Email))
        {
            var newEmail = userDto.Email;
            
            if (userDatabase.Email != newEmail)
            {
                var emailExists = await this.GetByEmailAsync(newEmail);
                
                if (emailExists != null && emailExists.Id != userId)
                    return null; 
                
                userDatabase.Email = newEmail;
            }
        }
        
        if (!string.IsNullOrWhiteSpace(userDto.UserName))
            userDatabase.Username = userDto.UserName;
        
        userDatabase.UpdatedAt = DateTime.UtcNow;
        
        var result = _db.Users.Update(userDatabase);
        await _db.SaveChangesAsync();
        return new UserDto
        (
            Id: result.Entity.Id,
            UserName: result.Entity.Username,
            Role: result.Entity.Role,
            Email: result.Entity.Email,
            CreatedAt: result.Entity.CreatedAt
        );
    }

    public async Task<User?> DeleteAsync(int id)
    {
        var userDatabase = await this.GetEntityByIdAsync(id);

        if (userDatabase == null)
            return null;

        _db.Users.Remove(userDatabase);
        await _db.SaveChangesAsync();
        return userDatabase;
    }
}