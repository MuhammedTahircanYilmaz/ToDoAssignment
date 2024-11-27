using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAssignment.Models.Users.Dtos.Requests;

using ToDoAssignment.Service.Services.Users.Abstracts;

namespace ToDoAssignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {

        [HttpGet("getbyemail")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var result = await _userService.GetByEmailAsync(email);
            return Ok(result);
        }
        
        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var result = await _userService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequestDto dto)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userService.ChangePasswordAsync(userId, dto);
            return Ok(result);
        }

    }
}
