using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAssignment.Models.Users.Dtos.Requests;
using ToDoAssignment.Service.Authentications.Services.Abstracts;

namespace ToDoAssignment.WebApi.Controllers
{
    [Route("api/Users/")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserRequestDto dto)
        {
            var result = await _authenticationService.RegisterByTokenAsync(dto);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authenticationService.LoginByTokenAsync(dto);
            return Ok(result);
        }
    }
}
