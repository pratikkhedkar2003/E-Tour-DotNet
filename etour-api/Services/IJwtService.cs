
using etour_api.Dtos;

namespace etour_api.Services;

public interface IJwtService
{
    string GenerateToken(UserDto user);
}