namespace ToDoAssignment.Models.Dtos.Categories.Requests;

public sealed record UpdateCategoryRequestDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
