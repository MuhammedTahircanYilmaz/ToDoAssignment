using Core.Repository;
using ToDoAssignment.Models.Categories.Entity;
using ToDoAssignment.Repository.Categories.Repositories.Abstracts;
using ToDoAssignment.Repository.Contexts;

namespace ToDoAssignment.Repository.Categories.Repositories.Concretes;

public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, Guid>, ICategoryRepository
{
    public EfCategoryRepository(BaseDbContext context) : base(context)
    {
      
    }
}


