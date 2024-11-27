using ToDoAssignment.Models.Tokens.Response;
using ToDoAssignment.Models.Users.Entity;

namespace ToDoAssignment.Service.JWT.Services.Abstracts;

public interface IJwtService
{
    Task<TokenResponseDto> CreateToken(User user);
}
