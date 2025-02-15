
using etour_api.Dtos;

namespace etour_api.Services;

public interface IPaymentService
{
    Task<string> CreateCheckoutSession(TourBookingDto tourBookingDto);
}