using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleGuard.Api.ViewModels;
using VehicleGuard.Shared.DTOs.Vehicles;
using VehicleGuard.Shared.Interfaces.Vehicle;
using VehicleGuard.Api.Extensions;

namespace VehicleGuard.Api.Controllers.Vehicles;

[ApiController]
[Authorize]
[Route("v1/[Controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;
    
    public VehiclesController(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create(CreateVehicleDto vehicleDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var userId = User.GetUserId();
        
        if(userId == null)
            return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));
        
        try
        {
            var result = await _vehicleRepository.CreateAsync(vehicleDto, userId.Value);

            if(result == null)
                return Conflict(new ResultViewModel<object>("License plate already exists."));
            
            var vehicleResponse = new VehicleDto
            (
                Id: result.Id,
                Brand: result.Brand,
                Model: result.Model,
                Color: result.Color,
                LicensePlate: result.LicensePlate
            );

            return Created("", new ResultViewModel<VehicleDto>(vehicleResponse));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error!"));
        }
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));
            
            var result = await _vehicleRepository.GetAllAsync(userId.Value);

            return Ok(new ResultViewModel<List<VehicleDto>>(result));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error!"));
        }
    }

    [HttpGet]
    [Route("{vehicleId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int vehicleId)
    {
        try
        {
            var userId = User.GetUserId();
            
            if (userId == null)
                return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));

            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, userId.Value);
            
            if(vehicle == null)
                return NotFound(new ResultViewModel<object>($"Vehicle with id {vehicleId} not found."));

            var vehicleDto = new VehicleDto
            (
                Id: vehicle.Id,
                Brand: vehicle.Brand,
                Model: vehicle.Model,
                Color: vehicle.Color,
                LicensePlate: vehicle.LicensePlate
            );

            return Ok(new ResultViewModel<VehicleDto>(vehicleDto));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error!"));
        }
    }

    [HttpDelete]
    [Route("{vehicleId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int vehicleId)
    {
        var userId = User.GetUserId();
            
        if (userId == null)
            return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));

        try
        {
            var result = await _vehicleRepository.DeleteAsync(vehicleId, userId.Value);
            if(result == false)
                return NotFound(new ResultViewModel<object>($"Vehicle with id {vehicleId} not found."));
            return NoContent();
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error!"));
        }
    }

    [HttpPut]
    [Route("{vehicleId:int}")]
    public async Task<IActionResult> Update([FromRoute] int vehicleId, [FromBody] UpdateVehicleDto vehicle)
    {
        var userId = User.GetUserId();
            
        if (userId == null)
            return StatusCode(500, new ResultViewModel<object>("User not logged in or user id is not into the token"));
        try
        {
            var result = await _vehicleRepository.UpdateAsync(vehicle, vehicleId, userId.Value);
            if (result == null)
                return NotFound(new ResultViewModel<object>($"Update failed, vehicle id {vehicleId} not found!"));
            return Ok(new ResultViewModel<object>(result));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error!"));
        }
    }
    
}