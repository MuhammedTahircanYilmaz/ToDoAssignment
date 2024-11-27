namespace ToDoAssignment.Models.Users.Dtos.Requests;

public sealed record ChangePasswordRequestDto
{
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }
}
