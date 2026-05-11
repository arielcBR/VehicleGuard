using Microsoft.AspNetCore.Mvc;
using VehicleGuard.Shared.DTOs.Users;
using VehicleGuard.Api.Extensions;
using VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Shared.Domain.Enums;
using VehicleGuard.Shared.Interfaces.Users;
using VehicleGuard.Api.ViewModels;


namespace VehicleGuard.Api.Controllers.Users;

[ApiController]
[Route("v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var listOfUsers = await _userRepository.Get();

        if (listOfUsers == null || listOfUsers.Count == 0)
            return Ok(new ResultViewModel<List<UserDto>>(new List<UserDto>()));

        return Ok(new ResultViewModel<List<UserDto>>(listOfUsers));
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<object>(ModelState.GetErrors()));

        var userExists = await _userRepository.GetByEmailAsync(userDto.Email);
        if (userExists != null)
            return Conflict(new ResultViewModel<object>("Email já cadastrado!"));


        var newUser = new User
        {
            Username = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            PasswordHash = "", 
            Role = Role.User, 
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            var userCreated = await _userRepository.CreateAsync(newUser);
            return Created("", new ResultViewModel<UserDto>(userCreated));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var userDatabase = await _userRepository.GetByIdAsync(id);
            if(userDatabase == null)
                return NotFound(new ResultViewModel<object>("User not found"));
            
            return Ok(new ResultViewModel<UserDto>(userDatabase));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Server internal error"));
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateUserDto userDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<object>(ModelState.GetErrors()));
        
        try
        {
            var userUpdated = await _userRepository.UpdateAsync(id, userDto);
            
            if (userUpdated == null)
                return NotFound(new ResultViewModel<object>("User not found"));
            
            return Ok(new ResultViewModel<UserDto>(userUpdated));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Server internal error"));
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var result = await _userRepository.DeleteAsync(id);

            if (result == null)
                return NotFound(new ResultViewModel<object>("User not found!"));
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }
}