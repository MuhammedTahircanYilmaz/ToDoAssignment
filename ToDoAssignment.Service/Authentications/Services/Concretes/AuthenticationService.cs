
using ToDoAssignment.Models.Tokens.Response;
using ToDoAssignment.Models.Users.Dtos.Requests;
using ToDoAssignment.Service.Authentications.Services.Abstracts;
using ToDoAssignment.Service.JWT.Services.Abstracts;
using ToDoAssignment.Service.Services.Users.Abstracts;

namespace ToDoAssignment.Service.Authentications.Services.Concretes;

public class AuthenticationService(IUserService userService, IJwtService jwtService) : IAuthenticationService
{

    public async Task<TokenResponseDto> RegisterByTokenAsync(RegisterUserRequestDto dto)
    {
        var registerResponse = await userService.RegisterUserAsync(dto);
        var tokenResponse = await jwtService.CreateToken(registerResponse);
        return tokenResponse;
    }

    public async Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto)
    {
        var loginResponse = await userService.LoginAsync(dto);
        var tokenResponse = await jwtService.CreateToken(loginResponse);
        return tokenResponse;
    }


}
