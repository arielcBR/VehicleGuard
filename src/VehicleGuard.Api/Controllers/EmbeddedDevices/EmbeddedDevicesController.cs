using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleGuard.Shared.DTOs.EmbeddedDevices;
using VehicleGuard.Api.Extensions;
using VehicleGuard.Shared.Interfaces.EmbeddedDevices;
using VehicleGuard.Api.ViewModels;

namespace VehicleGuard.Api.Controllers.EmbeddedDevices;

[ApiController]
[Route("v1/[controller]")]
[Authorize]
public class EmbeddedDevicesController : ControllerBase
{
    private readonly IEmbeddedDeviceRepository _embeddedDeviceRepository;

    public EmbeddedDevicesController(IEmbeddedDeviceRepository embeddedDeviceRepository)
    {
        _embeddedDeviceRepository = embeddedDeviceRepository;
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create([FromBody] CreateEmbeddedDeviceDto deviceEmbeddedDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var userId = User.GetUserId();

        if (userId == null)
            return BadRequest(new ResultViewModel<object>("User not found or not logged in!"));

        try
        {
            var vehicleHasEmbeddedDeviceInstalled = await _embeddedDeviceRepository.HasDeviceInstalledAsync(deviceEmbeddedDto.VehicleId);

            if (vehicleHasEmbeddedDeviceInstalled == true)
                return Conflict(new ResultViewModel<object>("Vehicle already has an embedded device installed!"));
            
            var deviceCreated = await _embeddedDeviceRepository.CreateAsync(deviceEmbeddedDto, userId.Value);
            
            if(deviceCreated == null)
                return NotFound(new ResultViewModel<object>($"Vehicle does not exist or it does not belong to the user id {userId.Value}"));
            
            return Created("", new ResultViewModel<EmbeddedDeviceDto>(deviceCreated));
        }
        catch
        {
            return StatusCode(500,new ResultViewModel<object>("Internal server error"));
        }
    }

    [HttpGet]
    [Route("{embeddedDeviceId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int embeddedDeviceId)
    {
        var userId = User.GetUserId();

        if (userId == null)
            return BadRequest(new ResultViewModel<object>("User not found or not logged in!"));

        try
        {
            var embeddedDevice = await _embeddedDeviceRepository.GetByIdAsync(embeddedDeviceId, userId.Value);
            
            if (embeddedDevice == null)
                return NotFound(new ResultViewModel<object>($"Embedded device with id {embeddedDeviceId} not found!"));
            
            return Ok(new ResultViewModel<EmbeddedDeviceDto>(embeddedDevice));
        }
        catch
        {
            return StatusCode(500,new ResultViewModel<object>("Internal server error"));
        }
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetUserId();

        if (userId == null)
            return BadRequest(new ResultViewModel<object>("User not found or not logged in!"));

        try
        {
            var listOfembeddedDevices = await _embeddedDeviceRepository.GetAllAsync(userId.Value);
            
            if (listOfembeddedDevices.Count == 0)
                return Ok(new ResultViewModel<List<EmbeddedDeviceDto>>(new List<EmbeddedDeviceDto>()));
            
            return Ok(new ResultViewModel<List<EmbeddedDeviceDto>>(listOfembeddedDevices));
        }
        catch
        {
            return StatusCode(500,new ResultViewModel<object>("Internal server error"));
        }
    }

    [HttpDelete]
    [Route("{embeddedDeviceId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int embeddedDeviceId)
    {
        var userId = User.GetUserId();

        if (userId == null)
            return BadRequest(new ResultViewModel<object>("User not found or not logged in!"));
        
        try
        {
            var result = await _embeddedDeviceRepository.DeleteAsync(embeddedDeviceId, userId.Value);
            
            if(!result)
                return NotFound(new ResultViewModel<object>($"Embedded device with id {embeddedDeviceId} not found!"));
            
            return NoContent();
        }
        catch
        {
            return StatusCode(500,new ResultViewModel<object>("Internal server error"));
        }
    }
}