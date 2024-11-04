using Core.Entities;
using ToDoAssignment.Models.Enums;

namespace ToDoAssignment.Models.Entities;

public sealed class ToDo : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public Guid CategoryId { get; set; }
    public bool Completed { get; set; }
    public Category Category { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public bool IsUpdateable { get; set; }
}