using Flight_API.Dto;
using Flight_API.Entities;
using Flight_API.Data;
namespace Flight_API.Contracts;

public interface IFlightService 
{
    Task<int> AddAsync(FlightDto flight);

    Task<IEnumerable<FlightDto>> GetAllAsync();

    Task<FlightDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(FlightDto flight);

    Task<bool> DeleteAsync(int id);

    Task<PagedResultDto<FlightDto>> GetAllPagedAsync(
        string? flightNumber,
        string? airlineName,
        string? sourceAirport,
        string? destinationAirport,
        bool? isActive,
        int pageNumber,
        int pageSize);
    
}