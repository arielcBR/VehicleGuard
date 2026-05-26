using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleGuard.Shared.DTOs.Gps;
using VehicleGuard.Api.Extensions;
using VehicleGuard.Api.ViewModels;
using VehicleGuard.Shared.Interfaces.Gps;

namespace VehicleGuard.Api.Controllers.Gps;

[ApiController]
[Route("v1/")]
[Authorize]
public class GpsController : ControllerBase
{
    private readonly IGpsRepository _gpsRepository;
    
    public GpsController(IGpsRepository gpsRepository)
    {
        _gpsRepository = gpsRepository;
    }
    
    [HttpPost]
    [Route("gps/")]
    public async Task<IActionResult> Create(
        [FromBody] GpsDto gpsDto)
    {
        var userId = User.GetUserId();
        
        if(userId == null)
            return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        try
        {
            var Gps = await _gpsRepository.CreateAsync(gpsDto, userId.Value);
            
            if(Gps == null)
                return BadRequest(new ResultViewModel<object>("GPS could not be created"));
            
            return Created("", new ResultViewModel<GpsDto>(Gps));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error"));
        }
    }

    [HttpGet]
    [Route("gps/embeddedDevice/{embeddedDeviceId:int}")]
    public async Task<IActionResult> Get([FromRoute] int embeddedDeviceId)
    {
        var userId = User.GetUserId();
        if(userId == null)
            return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));
        
        if(embeddedDeviceId <= 0)
            return BadRequest(new ResultViewModel<object>("Embedded device is not valid"));

        try
        {
            var gpsList = await _gpsRepository.GetAllByDeviceAsync(embeddedDeviceId, userId.Value);
            return gpsList == null 
                ? NotFound(new ResultViewModel<object>($"No GPS records found for device {embeddedDeviceId}")) 
                : Ok(new ResultViewModel<List<GpsDto>>(gpsList));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error"));
        }
    }
    
    [HttpGet]
    [Route("gps/{gpsId:int}/embeddedDevice/{embeddedDeviceId:int}/")]
    public async Task<IActionResult> GetById([FromRoute] int embeddedDeviceId, [FromRoute] int gpsId)
    {
        var userId = User.GetUserId();
        if(userId == null)
            return BadRequest(new ResultViewModel<object>("User not logged in or user id is not into the token"));
        
        if(embeddedDeviceId <= 0)
            return BadRequest(new ResultViewModel<object>("Embedded device is not valid"));

        try
        {
            var gps = await _gpsRepository.GetByIdAsync(embeddedDeviceId, gpsId, userId.Value);
            return gps == null ? NotFound(new ResultViewModel<object>($"Gps Id: {gpsId} not found!")) : Ok(new ResultViewModel<GpsDto>(gps));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<object>("Internal server error"));
        }
    }
}