using ToDoAssignment.Models.Roles.Dtos;

namespace ToDoAssignment.Service.Roles.Services.Abstracts;

public interface IRoleService
{

    Task<string> AddRoleToUser(AddRoleToUserRequestDto dto);

    Task<List<string>> GetAllRolesByUserId(string userId);

    Task<string> AddRoleAsync(string name);
}