
using etour_api.Dtos;
using etour_api.Models;
using etour_api.Payload.Request;

namespace etour_api.Services;

public interface ITourService
{
    Task<TourReview> AddTourReview(TourReviewRequest tourReviewRequest);
    Task<TourBookingDto> ConfirmTourBooking(string bookingReference);
    Task<TourBookingDto> CreateTourBooking(TourBookingRequest tourBookingRequest);
    Task<List<Tour>> GetAllTours();
    Task<List<Tour>> GetAllToursByTourSubcategoryId(ulong tourSubcategoryId);
    Task<List<Tour>> GetPopularTours();
    Task<TourBookingDto> GetTourBookingById(ulong bookingId);
    Task<List<TourBookingDto>> GetTourBookingsByUserId(ulong userId);
    Task<Tour> GetTourById(ulong tourId);
}