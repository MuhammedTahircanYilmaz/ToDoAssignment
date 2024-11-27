using ToDoAssignment.Models.Tokens.Response;
using ToDoAssignment.Models.Users.Dtos.Requests;

namespace ToDoAssignment.Service.Authentications.Services.Abstracts
{
    public interface IAuthenticationService
    {
        Task<TokenResponseDto> RegisterByTokenAsync(RegisterUserRequestDto dto);
        Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto);
    }
}
