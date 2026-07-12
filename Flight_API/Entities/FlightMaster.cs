using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_API.Entities;

[Table("FlightMaster")]
public class FlightMaster
{
    [Key]
    public int FlightId { get; set; }

    [Required]
    [StringLength(20)]
    public string FlightNumber { get; set; }

    [Required]
    [StringLength(100)]
    public string AirlineName { get; set; }

    [Required]
    [StringLength(50)]
    public string SourceAirport { get; set; }

    [Required]
    [StringLength(50)]
    public string DestinationAirport { get; set; }

    [Required]
    public DateTime DepartureTime { get; set; }

    [Required]
    public DateTime ArrivalTime { get; set; }

    [Required]
    public int TotalSeats { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TicketPrice { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(100)]
    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    [StringLength(100)]
    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}