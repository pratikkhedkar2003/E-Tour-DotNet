
using etour_api.Dtos;
using etour_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace etour_api.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("checkout/hosted")]
    public async Task<ActionResult<string>> CheckoutHosted([FromBody] TourBookingDto tourBookingDto) 
    {
        return await _paymentService.CreateCheckoutSession(tourBookingDto);
    }
}