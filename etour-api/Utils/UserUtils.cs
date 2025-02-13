
using etour_api.Dtos;
using etour_api.Models;
using etour_api.Payload.Request;

namespace etour_api.Utils;

public static class UserUtils
{
    public static User CreateNewUser(UserRequest userRequest)
    {
        return new User()
        {
            ReferenceId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = userRequest.FirstName,
            MiddleName = userRequest.MiddleName,
            LastName = userRequest.LastName,
            Email = userRequest.Email,
            Phone = string.Empty,
            Bio = string.Empty,
            ImageUrl = "https://cdn-icons-png.flaticon.com/512/149/149071.png",
            Role = "ROLE_USER",
            LastLogin = DateTime.Now,
            LoginAttempts = 0,
            Enabled = false,
            AccountNonExpired = true,
            AccountNonLocked = true,
        };
    }

    public static UserDto ToUserDto(User user, Address address)
    {
#pragma warning disable CS8629 // Nullable value type may be null.
        return new UserDto()
        {
            Id = user.Id,
            UserId = user.UserId,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Bio = user.Bio,
            ImageUrl = user.ImageUrl,
            LastLogin = user.LastLogin.ToString(),
            CreatedAt = user.CreatedAt.ToString(),
            UpdatedAt = user.UpdatedAt.ToString(),
            Role = user.Role,
            AccountNonExpired = (bool)user.AccountNonExpired,
            AccountNonLocked = (bool)user.AccountNonLocked,
            Enabled = user.Enabled,
            AddressLine = address.AddressLine,
            City = address.City,
            State = address.State,
            Country = address.Country,
            ZipCode = address.ZipCode
        };
#pragma warning restore CS8629 // Nullable value type may be null.
    }
}

