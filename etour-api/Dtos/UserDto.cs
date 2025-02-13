
namespace etour_api.Dtos;

public class UserDto
{
    public ulong Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public string? LastLogin { get; set; } = string.Empty;
    public string? CreatedAt { get; set; } = string.Empty;
    public string? UpdatedAt { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool AccountNonExpired { get; set; } = true;
    public bool AccountNonLocked { get; set; } = true;
    public bool Enabled { get; set; } = false;
    public string AddressLine { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
