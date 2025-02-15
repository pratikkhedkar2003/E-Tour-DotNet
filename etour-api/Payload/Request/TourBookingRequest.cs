
using System.ComponentModel.DataAnnotations;

namespace etour_api.Payload.Request;

public class TourBookingRequest
{
    [Required(ErrorMessage = "Tour ID cannot be empty or null")]
    public required ulong TourId { get; set; }

    [Required(ErrorMessage = "User ID cannot be empty or null")]
    public required ulong UserId { get; set; }

    [Required(ErrorMessage = "Departure ID cannot be empty or null")]
    public required ulong DepartureId { get; set; }

    [Required(ErrorMessage = "Total price cannot be empty or null")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than zero")]
    public required double TotalPrice { get; set; }

    [Required(ErrorMessage = "Passengers cannot be empty or null")]
    [MinLength(1, ErrorMessage = "At least one passenger is required")]
    public required List<PassengerRequest> Passengers { get; set; } = new();
}