using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using VehicleGuard.Shared.DTOs.Auth;
using VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Shared.Interfaces.Users;

namespace VehicleGuard.Api.Services;


public class TokenService
{
    private readonly IUserRepository _db;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<User> _passwordHasher;

    public TokenService(
        IUserRepository db, 
        IConfiguration configuration,
        IPasswordHasher<User> passwordHasher)
    {
        _db = db;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<LoginResponseDto?> GenerateToken(LoginDto credentials)
    {
        var userDatabase = await _db.GetByEmailAsync(credentials.Email);

        if (userDatabase == null)
            return null;
        
        var passwordValid = _passwordHasher.VerifyHashedPassword(userDatabase, userDatabase.PasswordHash, credentials.Password);
        if (passwordValid == PasswordVerificationResult.Failed)
            return null;
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]!);
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDatabase.Id.ToString()),
                new Claim(ClaimTypes.Name, userDatabase.Username), 
                new Claim(ClaimTypes.Email, userDatabase.Email), 
                new Claim(ClaimTypes.Role, userDatabase.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(securityToken);
        
        return new LoginResponseDto(jwt, securityToken.ValidTo);
    }
}