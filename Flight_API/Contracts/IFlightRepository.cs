using Flight_API.Dto;
using Flight_API.Entities;
namespace Flight_API.Contracts;

public interface IFlightRepository
{
    Task<int> AddAsync(FlightMaster flightMaster);

    Task<IEnumerable<FlightMaster>> GetAllAsync();

    Task<FlightMaster?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(FlightMaster flightMaster);

    Task<bool> DeleteAsync(int id);

    Task<PagedResultDto<FlightMaster>> GetAllPagedAsync(
        string? flightNumber,
        string? airlineName,
        string? sourceAirport,
        string? destinationAirport,
        bool? isActive,
        int pageNumber,
        int pageSize);
}