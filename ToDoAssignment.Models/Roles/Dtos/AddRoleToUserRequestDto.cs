namespace ToDoAssignment.Models.Roles.Dtos;

public sealed record AddRoleToUserRequestDto
{
    public string RoleName { get; init; }
    public string UserId { get; init; }
}
