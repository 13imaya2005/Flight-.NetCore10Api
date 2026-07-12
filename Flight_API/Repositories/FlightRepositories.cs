using Flight_API.Contracts;
using Flight_API.Dto;
using Flight_API.Entities;
using Flight_API.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlightAPI.Repositories;

public class FlightRepositories : IFlightRepository
{
    private readonly AppDbContext _dbContext;

    public FlightRepositories(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddAsync(FlightMaster flightMaster)
    {
        var result = await _dbContext.Database.ExecuteSqlRawAsync(
            @"EXEC sp_FlightMaster_Insert
                @FlightNumber,
                @AirlineName,
                @SourceAirport,
                @DestinationAirport,
                @DepartureTime,
                @ArrivalTime,
                @TotalSeats,
                @TicketPrice,
                @IsActive",

            new SqlParameter("@FlightNumber", flightMaster.FlightNumber),
            new SqlParameter("@AirlineName", flightMaster.AirlineName),
            new SqlParameter("@SourceAirport", flightMaster.SourceAirport),
            new SqlParameter("@DestinationAirport", flightMaster.DestinationAirport),
            new SqlParameter("@DepartureTime", flightMaster.DepartureTime),
            new SqlParameter("@ArrivalTime", flightMaster.ArrivalTime),
            new SqlParameter("@TotalSeats", flightMaster.TotalSeats),
            new SqlParameter("@TicketPrice", flightMaster.TicketPrice),
            new SqlParameter("@IsActive", (object?)flightMaster.IsActive ?? DBNull.Value)
        );

        return result;
    }

    public async Task<bool> UpdateAsync(FlightMaster flightMaster)
    {
        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
            @"EXEC sp_FlightMaster_Update
                @FlightId,
                @FlightNumber,
                @AirlineName,
                @SourceAirport,
                @DestinationAirport,
                @DepartureTime,
                @ArrivalTime,
                @TotalSeats,
                @TicketPrice,
                @IsActive",

            new SqlParameter("@FlightId", flightMaster.FlightId),
            new SqlParameter("@FlightNumber", flightMaster.FlightNumber),
            new SqlParameter("@AirlineName", flightMaster.AirlineName),
            new SqlParameter("@SourceAirport", flightMaster.SourceAirport),
            new SqlParameter("@DestinationAirport", flightMaster.DestinationAirport),
            new SqlParameter("@DepartureTime", flightMaster.DepartureTime),
            new SqlParameter("@ArrivalTime", flightMaster.ArrivalTime),
            new SqlParameter("@TotalSeats", flightMaster.TotalSeats),
            new SqlParameter("@TicketPrice", flightMaster.TicketPrice),
            new SqlParameter("@IsActive", (object?)flightMaster.IsActive ?? DBNull.Value)
        );

        return affectedRows > 0;
    }

    public async Task<FlightMaster?> GetByIdAsync(int id)
    {
        var items = await _dbContext.FlightMasters
            .FromSqlRaw("EXEC sp_FlightMaster_GetById @FlightId",
                new SqlParameter("@FlightId", id))
            .AsNoTracking()
            .ToListAsync();

        return items.FirstOrDefault();
    }

    public async Task<IEnumerable<FlightMaster>> GetAllAsync()
    {
        return await _dbContext.FlightMasters
            .FromSqlRaw("EXEC sp_FlightMaster_GetAll")
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
            "EXEC sp_FlightMaster_Delete @FlightId",
            new SqlParameter("@FlightId", id));

        return affectedRows > 0;
    }

    public async Task<PagedResultDto<FlightMaster>> GetAllPagedAsync(
        string? flightNumber,
        string? airlineName,
        string? sourceAirport,
        string? destinationAirport,
        bool? isActive,
        int pageNumber,
        int pageSize)
    {
        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_FlightMaster_GetPaged";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@FlightNumber", (object?)flightNumber ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@AirlineName", (object?)airlineName ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@SourceAirport", (object?)sourceAirport ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@DestinationAirport", (object?)destinationAirport ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@IsActive", (object?)isActive ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
            command.Parameters.Add(new SqlParameter("@PageSize", pageSize));

            using var reader = await command.ExecuteReaderAsync();

            var items = new List<FlightMaster>();

            while (await reader.ReadAsync())
            {
                items.Add(new FlightMaster
                {
                    FlightId = reader.GetInt32(0),
                    FlightNumber = reader.GetString(1),
                    AirlineName = reader.GetString(2),
                    SourceAirport = reader.GetString(3),
                    DestinationAirport = reader.GetString(4),
                    DepartureTime = reader.GetDateTime(5),
                    ArrivalTime = reader.GetDateTime(6),
                    TotalSeats = reader.GetInt32(7),
                    TicketPrice = reader.GetDecimal(8),
                    IsActive = reader.GetBoolean(9),
                    CreatedBy = reader.IsDBNull(10) ? null : reader.GetString(10),
                    CreatedDate = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                    UpdatedBy = reader.IsDBNull(12) ? null : reader.GetString(12),
                    UpdatedDate = reader.IsDBNull(13) ? null : reader.GetDateTime(13)
                });
            }

            await reader.NextResultAsync();

            int totalRecords = 0;

            if (await reader.ReadAsync())
            {
                totalRecords = reader.GetInt32(0);
            }

            return new PagedResultDto<FlightMaster>
            {
                Data = items,
                TotalRecords = totalRecords
            };
        }
    }
}