using Core.Repository;
using ToDoAssignment.Models.Categories.Entity;

namespace ToDoAssignment.Repository.Categories.Repositories.Abstracts;

public interface ICategoryRepository : IRepository<Category, Guid>
{
}
