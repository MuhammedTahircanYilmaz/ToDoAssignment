namespace ToDoAssignment.Models.Categories.Dtos.Requests;

public sealed record UpdateCategoryRequestDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
