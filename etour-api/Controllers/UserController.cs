
using etour_api.Dtos;
using etour_api.Utils;
using etour_api.Payload.Request;
using etour_api.Payload.Response;
using etour_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace etour_api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public UserController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Response>> RegisterUser([FromBody] UserRequest userRequest)
    {
        UserDto userDto = await _userService.CreateUser(userRequest);
        return CreatedAtAction(
            "GetUserProfile",
            new { id = userDto.Id },
            RequestUtils.GetResponse(path: "api/user/register", code: 201, message: "Account created successfully", data: new Dictionary<string, object> { { "user", userDto } })
        );
    }

    [HttpPost("login")]
    public async Task<ActionResult<Response>> LoginUser([FromBody] LoginRequest loginRequest) 
    {
        UserDto userDto = await _userService.LoginUser(loginRequest);
        string accessToken = _jwtService.GenerateToken(userDto);
        return Ok(RequestUtils.GetResponse(path: "api/user/login", code: 200, message: "Logged in successfully",
         data: new Dictionary<string, object> { { "user", userDto }, { "accessToken", accessToken } }));
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpGet("profile")]
    public async Task<ActionResult<Response>> GetUserProfile([FromQuery] ulong userId)
    {
        UserDto userDto = await _userService.GetUserProfile(userId);
        return Ok(RequestUtils.GetResponse(path: "api/user/profile", code: 200, message: "User profile fetched successfully", data: new Dictionary<string, object> { { "user", userDto } }));
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpPatch("update")]
    public async Task<ActionResult<Response>> UpdateUserProfile([FromQuery] ulong userId, [FromBody] UserRequest userRequest)
    {
        UserDto userDto = await _userService.UpdateUserProfile(userId, userRequest);
        return Ok(RequestUtils.GetResponse(path: "api/user/profile", code: 200, message: "Profile updated successfully", data: new Dictionary<string, object> { { "user", userDto } }));
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpPatch("password")]
    public async Task<ActionResult<Response>> UpdatePassword([FromQuery] ulong userId, [FromBody] PasswordRequest passwordRequest)
    {
        UserDto userDto = await _userService.UpdatePassword(userId, passwordRequest);
        return Ok(RequestUtils.GetResponse(path: "api/user/password", code: 200, message: "Password updated successfully", data: new Dictionary<string, object> { { "user", userDto } }));
    }

    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpGet("list")]
    public async Task<ActionResult<Response>> GetAllUsers()
    {
        List<UserDto> userDtos = await _userService.GetAllUsers();
        return Ok(RequestUtils.GetResponse(path: "api/user/list", code: 200, message: "Users fetched successfully", data: new Dictionary<string, object> { { "users", userDtos } }));
    }

    [Authorize(Roles = "ROLE_USER,ROLE_ADMIN")]
    [HttpPost("logout")]
    public Task<ActionResult<Response>> Logout()
    {
        return Task.FromResult<ActionResult<Response>>(Ok(RequestUtils.GetResponse(path: "api/user/logout", code: 200, message: "Logged out successfully")));
    }

}