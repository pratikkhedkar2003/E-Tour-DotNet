
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace etour_api.Payload.Request;

public class PasswordRequest
{
    [Required(ErrorMessage = "Password cannot be empty or null")]
    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "New password cannot be empty or null")]
    [JsonPropertyName("newPassword")]
    public required string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm password cannot be empty or null")]
    [JsonPropertyName("confirmNewPassword")]
    public required string ConfirmNewPassword { get; set; }
}