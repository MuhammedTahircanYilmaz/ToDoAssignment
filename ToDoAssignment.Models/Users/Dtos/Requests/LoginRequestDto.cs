namespace ToDoAssignment.Models.Users.Dtos.Requests;

public sealed record LoginRequestDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
}
