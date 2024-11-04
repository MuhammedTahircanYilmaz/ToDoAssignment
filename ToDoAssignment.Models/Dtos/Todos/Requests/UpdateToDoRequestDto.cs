using ToDoAssignment.Models.Enums;

namespace ToDoAssignment.Models.Dtos.Todos.Requests;

public sealed record UpdateToDoRequestDto
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public Priority? Priority { get; init; }
    public Guid? CategoryId { get; init; }
}
