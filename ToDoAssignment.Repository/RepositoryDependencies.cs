using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoAssignment.Repository.Categories.Repositories.Abstracts;
using ToDoAssignment.Repository.Categories.Repositories.Concretes;
using ToDoAssignment.Repository.Contexts;
using ToDoAssignment.Repository.ToDos.Repositories.Abstracts;
using ToDoAssignment.Repository.ToDos.Repositories.Concretes;

namespace ToDoAssignment.Repository;

public static class RepositoryDependencies
{

    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();
        services.AddScoped<IToDoRepository, EfToDoRepository>();
        services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        return services;
    }
}
