
namespace Core.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; set; }

    public DateTime TimeCreated { get; set; }

    public DateTime? TimeUpdated { get; set; }
}
