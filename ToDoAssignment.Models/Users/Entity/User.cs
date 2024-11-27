using Microsoft.AspNetCore.Identity;
using ToDoAssignment.Models.Categories.Entity;
using ToDoAssignment.Models.Todos.Entity;

namespace ToDoAssignment.Models.Users.Entity;

public sealed class User : IdentityUser
{
    public List<ToDo>? ToDos { get; set; }
    public List<Category>? Categories { get; set; }
}
