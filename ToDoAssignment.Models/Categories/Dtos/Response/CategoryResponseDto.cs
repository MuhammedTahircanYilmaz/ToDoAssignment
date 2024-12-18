﻿namespace ToDoAssignment.Models.Categories.Dtos.Response;

public sealed record CategoryResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Username { get; init; }
}
