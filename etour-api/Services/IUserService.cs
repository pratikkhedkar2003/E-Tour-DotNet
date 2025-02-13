
using etour_api.Dtos;
using etour_api.Models;
using etour_api.Payload.Request;

namespace etour_api.Services;

public interface IUserService
{
    Task<UserDto> CreateUser(UserRequest userRequest);
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> GetUserProfile(ulong userId);
    Task<UserDto> LoginUser(LoginRequest loginRequest);
    Task<UserDto> UpdatePassword(ulong userId, PasswordRequest passwordRequest);
    Task<UserDto> UpdateUserProfile(ulong userId, UserRequest userRequest);
}