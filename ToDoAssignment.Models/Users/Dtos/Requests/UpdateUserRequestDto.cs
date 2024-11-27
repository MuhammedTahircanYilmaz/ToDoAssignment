namespace ToDoAssignment.Models.Users.Dtos.Requests;

public sealed record UpdateUserRequestDto
{
    public string Username { get; init; }
}
