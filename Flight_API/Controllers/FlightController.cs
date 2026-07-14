using Flight_API.Contracts;
using Flight_API.Models;
using Flight_API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flight_API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FlightController : ControllerBase
{
    private readonly IFlightService _service;

    public FlightController(IFlightService service)
    {
        _service = service;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Add(FlightDto dto)
    {
        var id = await _service.AddAsync(dto);
        return Ok(id);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("GetById/{flightId}")]
    public async Task<IActionResult> GetById(int flightId)
    {
        var result = await _service.GetByIdAsync(flightId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("Update/{FlightId}")]
    public async Task<IActionResult> Update( int FlightId, FlightDto dto)
    {
        var result = await _service.UpdateAsync(dto);

        if (!result)
            return BadRequest();

        return Ok(result);
    }

    [HttpDelete("Delete/{flightId}")]
    public async Task<IActionResult> Delete(int flightId )
    {
        var result = await _service.DeleteAsync(flightId);

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("GetAllPaged")]
    public async Task<IActionResult> GetAllPaged(
        string? flightNumber,
        string? airlineName,
        string? sourceAirport,
        string? destinationAirport,
        bool? isActive,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _service.GetAllPagedAsync(
            flightNumber,
            airlineName,
            sourceAirport,
            destinationAirport,
            isActive,
            pageNumber,
            pageSize);

        return Ok(result);
    }
}