
using etour_api.Dtos;
using Stripe;
using Stripe.Checkout;

namespace etour_api.Services.Impl;

public class PaymentServiceImpl : IPaymentService
{
    private readonly string _stripeApiKey;
    private readonly string _clientBaseUrl;

    public PaymentServiceImpl(IConfiguration configuration)
    {
        _stripeApiKey = configuration["Stripe:SecretKey"]!;
        _clientBaseUrl = "http://localhost:5173"; // Change as per your frontend
        StripeConfiguration.ApiKey = _stripeApiKey;
    }

    public async Task<string> CreateCheckoutSession(TourBookingDto tourBookingDto)
    {
        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl = $"{_clientBaseUrl}/payment/success/{tourBookingDto.ReferenceId}",
            CancelUrl = $"{_clientBaseUrl}/payment/cancel/{tourBookingDto.ReferenceId}",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "INR",
                        UnitAmountDecimal = (decimal?)(tourBookingDto.TotalPrice * 100), // Convert to cents
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = tourBookingDto.TourName,
                            Metadata = new Dictionary<string, string>
                            {
                                { "booking_id", tourBookingDto.ReferenceId }
                            }
                        }
                    }
                }
            }
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return session.Url;
    }
}