
namespace etour_api.Dtos;

public class PassengerDto
{
    public required string FirstName { get; set; }
    public required string MiddleName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public required string DateOfBirth { get; set; } // Consider using DateTime for better date handling
    public int Age { get; set; }
    public required string Gender { get; set; }
    public required string PassengerType { get; set; }
    public double PassengerCost { get; set; }
}