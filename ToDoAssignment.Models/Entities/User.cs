using Microsoft.AspNetCore.Identity;

namespace ToDoAssignment.Models.Entities;

public sealed class User : IdentityUser
{
    public List<ToDo>? ToDos { get; set; }
    public List<Category>? Categories { get; set; }
}
