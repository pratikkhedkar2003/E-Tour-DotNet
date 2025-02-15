
using etour_api.Dtos;
using etour_api.Models;
using etour_api.Payload.Request;

namespace etour_api.Utils;

public static class TourUtils
{
    public static Passenger CreatePassenger(PassengerRequest passengerRequest) 
    {
        return new Passenger
        {
            ReferenceId = Guid.NewGuid().ToString(),
            FirstName = passengerRequest.FirstName,
            MiddleName = passengerRequest.MiddleName,
            LastName = passengerRequest.LastName,
            Email = passengerRequest.Email,
            Phone = passengerRequest.Phone,
            DateOfBirth = DateOnly.Parse(passengerRequest.DateOfBirth),
            Age = passengerRequest.Age,
            Gender = passengerRequest.Gender,
            PassengerType = passengerRequest.PassengerType,
            PassengerCost = passengerRequest.PassengerCost
        };
    }

    public static TourBookingDto ToTourBookingDto(
        Booking booking, TourCategory tourCategory, TourSubcategory tourSubcategory, Tour tour,
        User user, List<Passenger> passengers
    ) {
        return new TourBookingDto()
        {
            Id = booking.Id,
            ReferenceId = booking.ReferenceId,
            BookingDate = booking.BookingDate.ToString()!,
            BookingStatus = booking.BookingStatus,
            TotalPrice = booking.TotalPrice,
            CategoryName = tourCategory.CategoryName,
            SubCategoryName = tourSubcategory.SubCategoryName,
            TourId = tour.TourId,
            TourName = tour.TourName,
            Description = tour.Description,
            Duration = tour.Duration,
            StartDate = booking.Departure.StartDate.ToString(),
            EndDate = booking.Departure.EndDate.ToString(),
            User = UserUtils.ToUserDto(user, user.Addresses.First()),
            Passengers = GetPassengerDtoList(passengers)
        };
    }

    public static TourBookingDto ToTourBookingDto(Booking booking)
    {
        return new TourBookingDto()
        {
            Id = booking.Id,
            ReferenceId = booking.ReferenceId,
            BookingDate = booking.BookingDate.ToString()!,
            BookingStatus = booking.BookingStatus,
            TotalPrice = booking.TotalPrice
        };
    }

    private static List<PassengerDto> GetPassengerDtoList(List<Passenger> passengers)
    {
        return passengers.Select(ToPassengerDto).ToList();
    }

    private static PassengerDto ToPassengerDto(Passenger passenger)
    {
        return new PassengerDto()
        {
            FirstName = passenger.FirstName,
            MiddleName = passenger.MiddleName,
            LastName = passenger.LastName,
            Email = passenger.Email,
            Phone = passenger.Phone,
            DateOfBirth = passenger.DateOfBirth.ToString(),
            Age = passenger.Age,
            Gender = passenger.Gender,
            PassengerType = passenger.PassengerType,
            PassengerCost = passenger.PassengerCost
        };
    }
}