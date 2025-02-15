
using System.ComponentModel.DataAnnotations;

namespace etour_api.Payload.Request;

public class PassengerRequest
{
    [Required(ErrorMessage = "First name cannot be empty or null")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Middle name cannot be empty or null")]
    public required string MiddleName { get; set; }

    [Required(ErrorMessage = "Last name cannot be empty or null")]
    public required string LastName { get; set; }

    public string? Email { get; set; } // Optional field

    public string? Phone { get; set; } // Optional field

    [Required(ErrorMessage = "Date of birth cannot be empty or null")]
    public required string DateOfBirth { get; set; }

    [Required(ErrorMessage = "Age cannot be empty or null")]
    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120")]
    public required int Age { get; set; }

    [Required(ErrorMessage = "Gender cannot be empty or null")]
    public required string Gender { get; set; }

    [Required(ErrorMessage = "Passenger Type cannot be empty or null")]
    public required string PassengerType { get; set; }

    [Required(ErrorMessage = "Passenger Cost cannot be empty or null")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Passenger Cost must be greater than zero")]
    public double PassengerCost { get; set; }
}