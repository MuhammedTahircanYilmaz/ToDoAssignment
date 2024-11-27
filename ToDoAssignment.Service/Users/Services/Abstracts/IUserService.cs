using Core.Entities;
using ToDoAssignment.Models.Users.Dtos.Requests;
using ToDoAssignment.Models.Users.Dtos.Response;
using ToDoAssignment.Models.Users.Entity;

namespace ToDoAssignment.Service.Services.Users.Abstracts;

public interface IUserService
{
    Task<User> RegisterUserAsync(RegisterUserRequestDto dto);
    Task<User> LoginAsync(LoginRequestDto dto);
    Task<ReturnModel<NoData>> UpdateAsync(string id, UpdateUserRequestDto dto);
    Task<ReturnModel<NoData>> DeleteAsync(string id);
    Task<ReturnModel<UserResponseDto>> GetByEmailAsync(string email);
    Task<ReturnModel<UserResponseDto>> GetByUserIdAsync(string id);
    Task<ReturnModel<NoData>> ChangePasswordAsync(string id, ChangePasswordRequestDto dto);

}
