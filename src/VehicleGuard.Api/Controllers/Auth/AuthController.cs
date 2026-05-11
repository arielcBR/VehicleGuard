using Microsoft.AspNetCore.Mvc;
using VehicleGuard.Shared.DTOs.Auth;
using VehicleGuard.Api.Services;
using VehicleGuard.Api.ViewModels;

namespace VehicleGuard.Api.Controllers.Auth;

[ApiController]
[Route("v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto credentials)
    {
        var loginResponse = await _tokenService.GenerateToken(credentials);

        if (loginResponse == null)
            return Unauthorized(new ResultViewModel<Object>("Email or password are incorrect."));
        
        return Ok(new ResultViewModel<LoginResponseDto>(loginResponse));
    }
}