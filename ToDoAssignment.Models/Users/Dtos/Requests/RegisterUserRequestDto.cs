namespace ToDoAssignment.Models.Users.Dtos.Requests;

public sealed record RegisterUserRequestDto
{
    public string Username { get; init; }
    public string Email { get; set; }
    public string Password { get; set; }
}
