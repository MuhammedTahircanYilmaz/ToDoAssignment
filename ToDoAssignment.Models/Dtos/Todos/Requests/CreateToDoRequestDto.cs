using ToDoAssignment.Models.Entities;
using ToDoAssignment.Models.Enums;

namespace ToDoAssignment.Models.Dtos.Todos.Requests;

public sealed record CreateToDoRequestDto
{
    public string Title { get; init; }
    public string Description { get; init; }
    public Priority Priority { get; init; }
    public Guid CategoryId { get; init; }
    public bool Completed { get; init; }

}
