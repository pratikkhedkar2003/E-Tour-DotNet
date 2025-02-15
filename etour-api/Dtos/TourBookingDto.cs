
namespace etour_api.Dtos;

public class TourBookingDto
{
    public ulong Id { get; set; }
    public required string ReferenceId { get; set; }
    public required string BookingDate { get; set; } // Consider using DateTime instead of string for better date handling
    public required string BookingStatus { get; set; }
    public double TotalPrice { get; set; }

    public string? CategoryName { get; set; }
    public string? SubCategoryName { get; set; }

    public string? TourId { get; set; }
    public string? TourName { get; set; }
    public string? Description { get; set; }
    public string? Duration { get; set; } // Consider using TimeSpan if it's in hours/minutes
    public string? StartDate { get; set; } // Consider using DateTime
    public string? EndDate { get; set; } // Consider using DateTime

    public UserDto? User { get; set; } // Assuming you have a DTO for User

    public List<PassengerDto> Passengers { get; set; } = new List<PassengerDto>();
}