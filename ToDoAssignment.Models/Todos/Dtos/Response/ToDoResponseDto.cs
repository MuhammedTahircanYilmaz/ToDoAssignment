using ToDoAssignment.Models.Todos.Enums;

namespace ToDoAssignment.Models.Todos.Dtos.Response;

public sealed record ToDoResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Priority Priority { get; init; }
    public string CategoryName { get; init; }
    public bool Completed { get; init; }
    public string UserName { get; init; }
}