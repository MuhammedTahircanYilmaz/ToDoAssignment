namespace ToDoAssignment.Models.Dtos.Users.Requests;

public sealed record ChangePasswordRequestDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
