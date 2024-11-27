using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAssignment.Models.Categories.Dtos.Requests;
using ToDoAssignment.Service.Categories.Abstracts;

namespace ToDoAssignment.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{

    [HttpPost("add")]
    public IActionResult Add([FromBody] AddCategoryRequestDto dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.Add(dto, userId);
        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody] UpdateCategoryRequestDto dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.Update(dto, userId);
        return Ok(result);
    }

    [HttpDelete("delete/{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.Delete(id, userId);
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
    public IActionResult GetAllByUserId()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.GetAllByUserId(userId);
        return Ok(result);
    }

    [HttpGet("byid /{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = categoryService.GetById(id, userId);
        return Ok(result);
    }

    [HttpGet("bytitle/{text}")]
    public IActionResult GetAllByTitleContains([FromRoute] string text)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = categoryService.GetAllByTitleContains(text, userId);
        return Ok(result);
    }
}
