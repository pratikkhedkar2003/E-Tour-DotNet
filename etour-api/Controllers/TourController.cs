
using etour_api.Dtos;
using etour_api.Models;
using etour_api.Payload.Request;
using etour_api.Payload.Response;
using etour_api.Services;
using etour_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace etour_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly ITourService _tourService;

    public TourController(ITourService tourService)
    {
        _tourService = tourService;
    }

    [HttpGet]
    public async Task<ActionResult<Response>> GetAllTours()
    {
        List<Tour> tours = await _tourService.GetAllTours();
        return Ok(RequestUtils.GetResponse(path: "api/tour", code: 200, message: string.Empty, data: new Dictionary<string, object> { { "tours", tours } }));
    }

    [HttpGet("popular")]
    public async Task<ActionResult<Response>> GetPopularTours()
    {
        List<Tour> tours = await _tourService.GetPopularTours();
        return Ok(RequestUtils.GetResponse(path: "api/tour/popular", code: 200, message: "Popular Tours retrieved", data: new Dictionary<string, object> { { "tours", tours } }));
    }

    [HttpGet("toursubcategory/{tourSubcategoryId}")]
    public async Task<ActionResult<Response>> GetAllToursByTourSubcategoryId([FromRoute] ulong tourSubcategoryId)
    {
        List<Tour> tours = await _tourService.GetAllToursByTourSubcategoryId(tourSubcategoryId);
        return Ok(RequestUtils.GetResponse(path: "api/tour/toursubcategory/{tourSubcategoryId}", code: 200, message: "Tours retrieved", data: new Dictionary<string, object> { { "tours", tours } })); 
    }

    [HttpGet("{tourId}")]
    public async Task<ActionResult<Response>> GetTourById([FromRoute] ulong tourId)
    {
        Tour tour = await _tourService.GetTourById(tourId);
        return Ok(RequestUtils.GetResponse(path: "api/tour/{tourId}", code: 200, message: "Tour retrieved", data: new Dictionary<string, object> { { "tour", tour } })); 
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpPost("review")]
    public async Task<ActionResult<Response>> AddTourReview([FromBody] TourReviewRequest tourReviewRequest)
    {
        TourReview tourReview = await _tourService.AddTourReview(tourReviewRequest);
        return CreatedAtAction(
            "GetTourById",
            new { tourId = tourReview.Tour.Id },
            RequestUtils.GetResponse(path: "api/tour/review", code: 201, message: "Review added successfully", data: new Dictionary<string, object> { { "tourReview", tourReview } })
        );
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpPost("booking")]
    public async Task<ActionResult<Response>> CreateTourBooking([FromBody] TourBookingRequest tourBookingRequest)
    {
        TourBookingDto booking = await _tourService.CreateTourBooking(tourBookingRequest);
        return CreatedAtAction(
            "GetTourById",
            new { tourId = booking.TourId },
            RequestUtils.GetResponse(path: "api/tour/booking", code: 201, message: "Tour booking created successfully", data: new Dictionary<string, object> { { "tourBooking", booking } })
        );
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpGet("booking/{bookingId}")]
    public async Task<ActionResult<Response>> GetTourBookingById([FromRoute] ulong bookingId)
    {
        TourBookingDto tourBookingDto = await _tourService.GetTourBookingById(bookingId);
        return Ok(RequestUtils.GetResponse(path: "api/tour/booking/{bookingId}", code: 200, message: "Tour booking retrieved", data: new Dictionary<string, object> { { "tourBooking", tourBookingDto } }));
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpGet("booking/user/{userId}")]
    public async Task<ActionResult<Response>> GetTourBookingsByUserId([FromRoute] ulong userId)
    {
        List<TourBookingDto> tourBookingDtos = await _tourService.GetTourBookingsByUserId(userId);
        return Ok(RequestUtils.GetResponse(path: "api/tour/booking/user/{userId}", code: 200, message: "Tour bookings retrieved", data: new Dictionary<string, object> { { "tourBookings", tourBookingDtos } }));
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpPatch("booking/success/{bookingReference}")]
    public async Task<ActionResult<Response>> ConfirmTourBooking([FromRoute] string bookingReference)
    {
        TourBookingDto tourBookingDto = await _tourService.ConfirmTourBooking(bookingReference);
        return Ok(RequestUtils.GetResponse(path: "api/tour/booking/success/{bookingReference}", code: 200, message: "Booking successfully done.", data: new Dictionary<string, object> { { "tourBooking", tourBookingDto } }));
    }

}