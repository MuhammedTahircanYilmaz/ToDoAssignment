using Core.Entities;
using ToDoAssignment.Models.Dtos.Users.Requests;
using ToDoAssignment.Models.Dtos.Users.Response;
using ToDoAssignment.Models.Entities;

namespace ToDoAssignment.Service.Services.Users.Abstracts;

public interface IUserService
{
    Task<ReturnModel<NoData>> RegisterUserAsync(RegisterUserRequestDto dto);
    Task<User> LoginAsync(LoginRequestDto dto);
    Task<ReturnModel<NoData>> UpdateAsync(string id, UpdateUserRequestDto dto);
    Task<ReturnModel<NoData>> DeleteAsync(string id);
    Task<ReturnModel<UserResponseDto>> GetByEmailAsync(string email);
    Task<ReturnModel<UserResponseDto>> GetByUserIdAsync(string id);
    Task<ReturnModel<NoData>> ChangePasswordAsync(string id, ChangePasswordRequestDto dto);

}
