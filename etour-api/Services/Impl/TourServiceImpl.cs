
using etour_api.Dtos;
using etour_api.Enums;
using etour_api.Exceptions;
using etour_api.Models;
using etour_api.Payload.Request;
using etour_api.Repositories;
using etour_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Services.Impl;

public class TourServiceImpl : ITourService
{
    private readonly AppDbContext _appDbContext;
    private readonly IEmailService _emailService;

    public TourServiceImpl(AppDbContext appDbContext, IEmailService emailService)
    {
        _appDbContext = appDbContext;
        _emailService = emailService;
    }

    public async Task<TourReview> AddTourReview(TourReviewRequest tourReviewRequest)
    {
        Tour tour = await _appDbContext.Tours.FirstOrDefaultAsync(t => t.Id == tourReviewRequest.TourId) ?? throw new ApiException("Tour not found.");
        User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == tourReviewRequest.UserId) ?? throw new ApiException("User not found.");

        TourReview tourReview = new TourReview()
        {
            ReferenceId = Guid.NewGuid().ToString(),
            Tour = tour,
            User = user,
            Review = tourReviewRequest.Review,
            Rating = tourReviewRequest.Rating
        };

        var savedTourReview = await _appDbContext.TourReviews.AddAsync(tourReview);
        await _appDbContext.SaveChangesAsync();
        return savedTourReview.Entity;
    }

    public async Task<List<Tour>> GetAllTours()
    {
        return await _appDbContext.Tours.Include(t => t.Departures).Include(t => t.Itineraries)
        .Include(t => t.TourPrices).Include(t => t.TourReviews).ToListAsync();
    }

    public async Task<List<Tour>> GetAllToursByTourSubcategoryId(ulong tourSubcategoryId)
    {
        TourSubcategory tourSubcategory = await _appDbContext.TourSubcategories.FirstOrDefaultAsync(t => t.Id == tourSubcategoryId) ?? throw new ApiException("Tour Subcategory not found.");
        return await _appDbContext.Tours.Include(t => t.Departures).Include(t => t.Itineraries)
        .Include(t => t.TourPrices).Include(t => t.TourReviews).Where(t => t.TourSubcategory.Id == tourSubcategoryId).ToListAsync();
    }

    public Task<List<Tour>> GetPopularTours()
    {
        return _appDbContext.Tours.Include(t => t.Departures).Include(t => t.Itineraries)
        .Include(t => t.TourPrices).Include(t => t.TourReviews).Where(t => t.IsPopular != 0).ToListAsync();
    }

    public async Task<Tour> GetTourById(ulong tourId)
    {
        return await _appDbContext.Tours.Include(t => t.Departures).Include(t => t.Itineraries)
        .Include(t => t.TourPrices).Include(t => t.TourReviews).FirstOrDefaultAsync(t => t.Id == tourId) ?? throw new ApiException("Tour not found.");
    }

    public async Task<TourBookingDto> CreateTourBooking(TourBookingRequest tourBookingRequest)
    {
        Booking savedBooking = await SaveBooking(tourBookingRequest);
        List<Passenger> savedPassengers = await SavePassengers(tourBookingRequest.Passengers, savedBooking);
        return TourUtils.ToTourBookingDto(
            booking: savedBooking,
            tourCategory: savedBooking.Tour.TourSubcategory.TourCategory,
            tourSubcategory: savedBooking.Tour.TourSubcategory,
            tour: savedBooking.Tour,
            user: savedBooking.User,
            passengers: savedPassengers
        );
    }

    public async Task<TourBookingDto> GetTourBookingById(ulong bookingId)
    {
        Booking booking = await GetTourBookingByBookingId(bookingId);
        List<Passenger> passengers = await GetPassengersByBooking(booking);

        return TourUtils.ToTourBookingDto(
            booking: booking,
            tourCategory: booking.Tour.TourSubcategory.TourCategory,
            tourSubcategory: booking.Tour.TourSubcategory,
            tour: booking.Tour,
            user: booking.User,
            passengers: passengers
        );
    }

    private async Task<List<Passenger>> GetPassengersByBooking(Booking booking)
    {
        return await _appDbContext.Passengers.Where(p => p.Booking.Id == booking.Id).ToListAsync();
    }

    private async Task<Booking> GetTourBookingByBookingId(ulong bookingId)
    {
        return await _appDbContext.Bookings.Include(b => b.Tour.TourSubcategory.TourCategory).Include(b => b.Tour.TourSubcategory)
        .Include(b => b.Tour).Include(b => b.User).Include(b => b.User.Addresses).Include(b => b.Departure).FirstOrDefaultAsync(b => b.Id == bookingId) 
        ?? throw new ApiException("Booking not found.");
    }

    private async Task<List<Passenger>> SavePassengers(List<PassengerRequest> passengers, Booking savedBooking)
    {
        foreach (PassengerRequest passengerRequest in passengers)
        {
            Passenger passenger = TourUtils.CreatePassenger(passengerRequest);
            passenger.Booking = savedBooking;
            passenger.Tour = savedBooking.Tour;
            await _appDbContext.Passengers.AddAsync(passenger);
            await _appDbContext.SaveChangesAsync();
        }

        return await _appDbContext.Passengers.Where(p => p.Booking.Id == savedBooking.Id).ToListAsync();
    }

    private async Task<Booking> SaveBooking(TourBookingRequest tourBookingRequest)
    {
        Tour tour = await _appDbContext.Tours.Include(t => t.TourSubcategory).Include(t => t.TourSubcategory.TourCategory)
        .FirstOrDefaultAsync(t => t.Id == tourBookingRequest.TourId) ?? throw new ApiException("Tour not found.");
        User user = await _appDbContext.Users.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.Id == tourBookingRequest.UserId) ?? throw new ApiException("User not found.");
        Departure departure = await _appDbContext.Departures.FirstOrDefaultAsync(d => d.Id == tourBookingRequest.DepartureId) ?? throw new ApiException("Departure not found.");

        Booking booking = new Booking()
        {
            ReferenceId = Guid.NewGuid().ToString(),
            BookingDate = DateTime.Now,
            BookingStatus = BookingStatus.PENDING.ToString(),
            TotalPrice = tourBookingRequest.TotalPrice,
            Tour = tour,
            User = user,
            Departure = departure
        };

        var savedBooking = await _appDbContext.Bookings.AddAsync(booking);
        await _appDbContext.SaveChangesAsync();
        return savedBooking.Entity;
    }

    public async Task<List<TourBookingDto>> GetTourBookingsByUserId(ulong userId)
    {
        User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new ApiException("User not found.");
        List<Booking> bookings = await _appDbContext.Bookings.Where(b => b.User.Id == user.Id).ToListAsync();
        return bookings.Select(TourUtils.ToTourBookingDto).ToList();
    }

    public async Task<TourBookingDto> ConfirmTourBooking(string bookingReference)
    {
        Booking booking = await _appDbContext.Bookings.Include(b => b.Tour.TourSubcategory.TourCategory).Include(b => b.Tour.TourSubcategory)
        .Include(b => b.Tour).Include(b => b.User).Include(b => b.User.Addresses).Include(b => b.Departure).FirstOrDefaultAsync(b => b.ReferenceId == bookingReference) 
        ?? throw new ApiException("Booking not found.");
        List<Passenger> passengers = await GetPassengersByBooking(booking);
        booking.BookingStatus = BookingStatus.CONFIRMED.ToString();
        await _appDbContext.SaveChangesAsync();
        TourBookingDto tourBookingDto = TourUtils.ToTourBookingDto(
            booking: booking,
            tourCategory: booking.Tour.TourSubcategory.TourCategory,
            tourSubcategory: booking.Tour.TourSubcategory,
            tour: booking.Tour,
            user: booking.User,
            passengers: passengers
        );
        await _emailService.SendBookingPdfEmail(tourBookingDto);
        return tourBookingDto;
    }
}