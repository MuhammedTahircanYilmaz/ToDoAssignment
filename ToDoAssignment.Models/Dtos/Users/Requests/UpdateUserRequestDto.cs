namespace ToDoAssignment.Models.Dtos.Users.Requests;

public sealed record UpdateUserRequestDto
{
    public string Username { get; init; }
}
