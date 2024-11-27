using ToDoAssignment.Models.Todos.Enums;

namespace ToDoAssignment.Models.Todos.Dtos.Requests;

public sealed record CreateToDoRequestDto
{
    public string Title { get; init; }
    public string Description { get; init; }
    public Priority Priority { get; init; }
    public Guid CategoryId { get; init; }

}
