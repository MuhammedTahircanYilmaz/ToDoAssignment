namespace ToDoAssignment.Models.Users.Dtos.Response;

public sealed record UserResponseDto
{
    public string userId { get; init; }
    public string Email { get; set; }
    public string Username { get; set; }
}