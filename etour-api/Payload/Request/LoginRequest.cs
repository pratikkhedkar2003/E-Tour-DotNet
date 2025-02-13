
using System.ComponentModel.DataAnnotations;

namespace etour_api.Payload.Request;

public class LoginRequest
{
    [Required(ErrorMessage = "Email cannot be empty or null")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password cannot be empty or null")]
    public required string Password { get; set; }
}