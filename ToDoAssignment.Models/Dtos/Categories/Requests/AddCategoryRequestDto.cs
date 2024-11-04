namespace ToDoAssignment.Models.Dtos.Categories.Requests;

public sealed record AddCategoryRequestDto
{
    public string Name { get; init; }
    public string UserId { get; init; }
}
