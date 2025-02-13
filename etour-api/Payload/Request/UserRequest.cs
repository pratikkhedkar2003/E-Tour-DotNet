
using System.ComponentModel.DataAnnotations;

namespace etour_api.Payload.Request;

public class UserRequest
{
    [Required(ErrorMessage = "First name cannot be empty or null")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Middle name cannot be empty or null")]
    public string MiddleName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name cannot be empty or null")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email cannot be empty or null")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Phone { get; set; }
    public string? Bio { get; set; }
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
}
