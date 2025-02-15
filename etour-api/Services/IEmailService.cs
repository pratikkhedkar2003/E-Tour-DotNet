
using etour_api.Dtos;

namespace etour_api.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendEmailAsync(string to, string subject, string body, bool isHtml);
    Task SendBookingPdfEmail(TourBookingDto tourBookingDto);
}