using Core.Entities;

namespace ToDoAssignment.Models.Entities;

public sealed class Category : Entity<Guid>
{
    public string Name { get; set; }
    public List<ToDo>? ToDos { get; set; }
    public string UserId { get; set; }
    public User User { get; init; }
}
