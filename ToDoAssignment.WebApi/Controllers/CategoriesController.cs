using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAssignment.Models.Dtos.Categories.Requests;
using ToDoAssignment.Service.Services.Categories.Abstracts;

namespace ToDoAssignment.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost("add")]
    [Authorize]
    public IActionResult Add([FromBody] AddCategoryRequestDto dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.Add(dto, userId);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize]
    public IActionResult Update([FromBody] UpdateCategoryRequestDto dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.Update(dto, userId);
        return Ok(result);
    }

    [HttpDelete("delete/{id:guid}")]
    [Authorize]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var result = categoryService.Delete(id);
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize (Roles = "Admin")]
    public IActionResult GetAll()
    {

        var result = categoryService.GetAll();
        return Ok(result);
    }

    [HttpGet("byuser")]
    [Authorize]
    public IActionResult GetAllByUserId()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.
    }
}
