using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAssignment.Models.Todos.Dtos.Requests;
using ToDoAssignment.Models.Todos.Enums;
using ToDoAssignment.Service.Todos.Abstracts;

namespace ToDoAssignment.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ToDosController(IToDoService toDoService) : ControllerBase
{
    [HttpPost("add")]
    public IActionResult Add([FromBody] CreateToDoRequestDto dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.Add(dto, userId);
        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody] UpdateToDoRequestDto dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.Update(dto, userId);

        return Ok(result);
    }

    [HttpDelete("delete/{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.Delete(id, userId);
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        var result = toDoService.GetAll();
        return Ok(result);
    }


    [HttpGet("byid/{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.GetById(id, userId);
        return Ok(result);
    }

    [HttpGet("allusertodos")]
    public IActionResult GetAllByUserId()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = toDoService.GetAllByUserId(userId);

        return Ok(result);
    }
    [HttpGet("bypriority/{priority}")]
    public IActionResult GetAllByPriority([FromRoute] Priority priority)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = toDoService.GetAllByPriority(priority, userId);

        return Ok(result);
    }


    [HttpGet("bystatus/completed/{completed:bool}")]
    public IActionResult GetAllByStatus([FromRoute] bool completed)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.GetAllByStatus(completed, userId);
        return Ok(result);
    }

    [HttpGet("bytitle")]
    public IActionResult GetAllByTitleHas([FromQuery]string text)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.GetAllByTitleHas(text, userId);
        return Ok(result);
    }

    [HttpGet("bycategory")]
    public IActionResult GetAllByCategoryId([FromQuery]Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = toDoService.GetAllByCategory(id, userId);
        return Ok(result);
    }

}
