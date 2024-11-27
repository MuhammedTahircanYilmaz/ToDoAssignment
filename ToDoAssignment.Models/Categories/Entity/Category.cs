using Core.Entities;
using ToDoAssignment.Models.Todos.Entity;
using ToDoAssignment.Models.Users.Entity;

namespace ToDoAssignment.Models.Categories.Entity;

public sealed class Category : Entity<Guid>
{
    public string Name { get; set; }
    public List<ToDo>? ToDos { get; set; }
    public string UserId { get; set; }
    public User User { get; init; }
}
