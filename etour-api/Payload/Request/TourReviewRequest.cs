
using System.ComponentModel.DataAnnotations;

namespace etour_api.Payload.Request;

public class TourReviewRequest
{
    [Required(ErrorMessage = "User ID is required")]
    public ulong UserId { get; set; }

    [Required(ErrorMessage = "Tour ID is required")]
    public ulong TourId { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public required int Rating { get; set; }

    [StringLength(500, ErrorMessage = "Review cannot exceed 500 characters")]
    public required string Review { get; set; }
}