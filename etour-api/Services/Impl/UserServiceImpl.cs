
using BCrypt.Net;
using etour_api.Dtos;
using etour_api.Exceptions;
using etour_api.Models;
using etour_api.Payload.Request;
using etour_api.Repositories;
using etour_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Services.Impl;

public class UserServiceImpl : IUserService
{
    private readonly AppDbContext _appDbContext;

    public UserServiceImpl(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<UserDto> CreateUser(UserRequest userRequest)
    {
        if (await _appDbContext.Users.AnyAsync(u => u.Email == userRequest.Email))
        {
            throw new ApiException("Email already exists. Please use a different email.");
        }

        User user = UserUtils.CreateNewUser(userRequest);
        user.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);

        var savedUser = await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();

        Address address = new Address()
        {
            ReferenceId = Guid.NewGuid().ToString(),
            AddressLine = string.Empty,
            City = string.Empty,
            State = string.Empty,
            Country = string.Empty,
            ZipCode = string.Empty,
            User = savedUser.Entity,
        };

        var savedAddress = await _appDbContext.Addresses.AddAsync(address);
        await _appDbContext.SaveChangesAsync();

        return UserUtils.ToUserDto(savedUser.Entity, savedAddress.Entity);
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _appDbContext.Users.Select(u => UserUtils.ToUserDto(u, u.Addresses.FirstOrDefault()!)).ToListAsync();
    }

    public async Task<UserDto> GetUserProfile(ulong userId)
    {
        User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new ApiException("User not found.");
        Address address = await GetAddressByUserIdAsync(user.Id);
        return UserUtils.ToUserDto(user, address);
    }

    public async Task<UserDto> LoginUser(LoginRequest loginRequest)
    {
        User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email) ?? throw new ApiException("Invalid Email or/and Password.");
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            throw new ApiException("Invalid Email or/and Password.");
        }
        Address address = await GetAddressByUserIdAsync(user.Id);
        return UserUtils.ToUserDto(user, address);
    }

    public async Task<UserDto> UpdatePassword(ulong userId, PasswordRequest passwordRequest)
    {
        if (passwordRequest.NewPassword != passwordRequest.ConfirmNewPassword)
        {
            throw new ApiException("Password and confirm password don't match. Please try again.");
        }

        User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new ApiException("User not found.");
        
        if (!BCrypt.Net.BCrypt.Verify(passwordRequest.Password, user.Password))
        {
            throw new ApiException("Invalid Current Password.");
        }
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(passwordRequest.NewPassword);

        var savedUser = _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync();

        Address address = await GetAddressByUserIdAsync(user.Id);

        return UserUtils.ToUserDto(savedUser.Entity, address);
    }

    public async Task<UserDto> UpdateUserProfile(ulong userId, UserRequest userRequest)
    {
        User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new ApiException("User not found.");
        user.FirstName = userRequest.FirstName;
        user.MiddleName = userRequest.MiddleName;
        user.LastName = userRequest.LastName;
        user.Email = userRequest.Email;
        user.Phone = userRequest.Phone;
        user.Bio = userRequest.Bio;

        var savedUser = _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync();

        Address address = await GetAddressByUserIdAsync(user.Id);

        address.AddressLine = userRequest.AddressLine!;
        address.City = userRequest.City!;
        address.State = userRequest.State!;
        address.Country = userRequest.Country!;
        address.ZipCode = userRequest.ZipCode!;

        var savedAddress = _appDbContext.Addresses.Update(address);
        await _appDbContext.SaveChangesAsync();

        return UserUtils.ToUserDto(savedUser.Entity, savedAddress.Entity);
    }

    private async Task<Address> GetAddressByUserIdAsync(ulong id)
    {
        return await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.User.Id == id) ?? throw new ApiException("Address not found.");
    }
}