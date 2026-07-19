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
    public async Task<IActionResult> Create(FlightDto dto)
    {
        try
        {
            var id = await _service.AddAsync(dto);

            return Ok(new ApiResponse<int>
            {
                Success = true,
                Message = "Item created successfully",
                Data = id
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error creating item",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<FlightDto>>
            {
                Success = true,
                Message = "Flight retrieved successfully",
                Data = data
            });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving Flight",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }
    [HttpGet("GetById/{FlightId}")]
    public async Task<IActionResult> GetById(int FlightId)
    {
        try
        {
            var item = await _service.GetByIdAsync(FlightId);

            return Ok(new ApiResponse<FlightDto>
            {
                Success = true,
                Message = "Flight retrieved successfully",
                Data = item
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving Flight",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPut("Update/{FlightId}")]
   
    public async Task<IActionResult> Update( int FlightId, FlightDto dto)
    {
        try
        {

            var updated = await _service.UpdateAsync(dto);

            if (!updated)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Flight not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Flight updated successfully"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error updating Flight",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }



    [HttpDelete("Delete/{FlightId}")]
   
    public async Task<IActionResult> Delete(int FlightId)
    {
        try
        {
            var deleted = await _service.DeleteAsync(FlightId);

            if (!deleted)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Item not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Item deleted successfully"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error deleting item",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
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
     try
            {
                var result = await _service.GetAllPagedAsync(
                    flightNumber, airlineName, sourceAirport, destinationAirport, isActive, pageNumber, pageSize);

                return Ok(new ApiResponse<IEnumerable<FlightDto>>
                {
                    Success = true,
                    Message = "Flight retrieved successfully",
                    Data = result.Data,
                    TotalRecords = result.TotalRecords
    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving Flight",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                     }
                });
            }
        }
    }



