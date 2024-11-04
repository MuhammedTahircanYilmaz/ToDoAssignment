using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ToDoAssignment.Models.Dtos.Users.Requests;
using ToDoAssignment.Models.Dtos.Users.Response;
using ToDoAssignment.Models.Entities;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Services.Users.Abstracts;

namespace ToDoAssignment.Service.Services.Users.Concretes;

public class EfUserService(IMapper mapper) : IUserService
{
    private readonly UserManager<User> _userManager;
    public async Task<ReturnModel<NoData>> RegisterUserAsync(RegisterUserRequestDto dto)
    {
        User user = new User()
        {
            Email = dto.Email,
            UserName = dto.Username
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 200, Messages.UserRegisteredMessage);
    }

    public async Task<User> LoginAsync(LoginRequestDto dto)
    {
        User userExist = null;
        
        if (dto.Email is not null)
        {
             userExist = await _userManager.FindByEmailAsync(dto.Email);
        }
        else if (dto.Username is not null)
        {
            userExist = await _userManager.FindByNameAsync(dto.Username);
        }

        UserIsPresent(userExist);

        var result = await _userManager.CheckPasswordAsync(userExist, dto.Password);
        if (result is false)
        {
            throw new BusinessException("Login failed, please check login information");
        }

        return userExist;

    }

    public async Task<ReturnModel<NoData>> UpdateAsync(string id, UpdateUserRequestDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);
        UserIsPresent(user);

        user.UserName = dto.Username;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded is false)
        {
            throw new BusinessException(result.Errors.First().Description);
        }

        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 200, Messages.UserUpdatedMessage);
    }

    public async  Task<ReturnModel<NoData>> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        UserIsPresent(user);
        await _userManager.DeleteAsync(user);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 204, Messages.UserDeletedMessage);
    }

    public async Task<ReturnModel<UserResponseDto>> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        UserIsPresent(user);
        
        var response = mapper.Map<UserResponseDto>(user);
        return ReturnModel<UserResponseDto>.ReturnModelOfSuccess(response, 200);
    }

    public async Task<ReturnModel<UserResponseDto>> GetByUserIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        UserIsPresent(user);

        var response = mapper.Map<UserResponseDto>(user);
        return ReturnModel<UserResponseDto>.ReturnModelOfSuccess(response, 200);
    }

    public async Task<ReturnModel<NoData>> ChangePasswordAsync(string id, ChangePasswordRequestDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);

        UserIsPresent(user);

        var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

        if (result.Succeeded is false)
        {
            throw new BusinessException(result.Errors.First().Description);
        }

        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 204, "Password has been changed successfully");

    }

    private void UserIsPresent(User? user)
    {
        if (user is null)
        {
            throw new NotFoundException("The User could not be found.");
        }
    }
}
