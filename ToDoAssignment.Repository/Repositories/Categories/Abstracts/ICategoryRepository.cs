using Core.Repository;
using ToDoAssignment.Models.Entities;

namespace ToDoAssignment.Repository.Repositories.Categories.Abstracts;

public interface ICategoryRepository : IRepository<Category,Guid>
{
}
