
using etour_api.Dtos;
using etour_api.Exceptions;
using etour_api.Utils;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace etour_api.Services.Impl;

public class EmailServiceImpl : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailServiceImpl(IConfiguration configuration)
    {
        _settings = configuration.GetSection("EmailSettings").Get<EmailSettings>()!;
    }

    public async Task SendBookingPdfEmail(TourBookingDto tourBookingDto)
    {
        try
        {
            byte[] pdfBytes = PdfDocumentUtils.GenerateBookingPdf(tourBookingDto);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
            email.To.Add(MailboxAddress.Parse(tourBookingDto?.User?.Email));
            email.Subject = "Booking Confirmation";

            var builder = new BodyBuilder();

            builder.TextBody = $"Hello {tourBookingDto?.User?.FirstName},\n\n" +
                               $"Please find your booking confirmation for {tourBookingDto?.TourName} attached.\n\n" +
                               "Regards,\nYour E-Tour Agency";

            builder.Attachments.Add("booking.pdf", pdfBytes, ContentType.Parse("application/pdf"));
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw new ApiException("Unable to send email");
        }
    }

    public async Task SendEmailAsync(string to, string subject, string body)
        => await SendEmailAsync(to, subject, body, false);

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        email.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain)
        {
            Text = body
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

public class EmailSettings
{
    public required string SmtpServer { get; set; }
    public int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string FromName { get; set; }
    public required string FromAddress { get; set; }
    public required string VerifyEmailHost { get; set; }
}