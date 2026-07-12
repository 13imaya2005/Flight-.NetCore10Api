using System;
namespace Flight_API.Dto
{
    public class FlightDto
    {
        public int FlightId { get; set; }
        public string? FlightNumber { get; set; }
        public string? AirlineName { get; set; }
        public string? SourceAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TotalSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}