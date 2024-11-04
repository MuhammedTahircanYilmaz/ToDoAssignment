using Core.Repository;
using ToDoAssignment.Models.Entities;
using ToDoAssignment.Repository.Contexts;
using ToDoAssignment.Repository.Repositories.Categories.Abstracts;

namespace ToDoAssignment.Repository.Repositories.Categories.Concretes;

public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, Guid>, ICategoryRepository
{
    public EfCategoryRepository(BaseDbContext context) : base(context)
    {

    }
}


