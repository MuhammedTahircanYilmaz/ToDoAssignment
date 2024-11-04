using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoAssignment.Repository.Contexts;
using ToDoAssignment.Repository.Repositories.Categories.Abstracts;
using ToDoAssignment.Repository.Repositories.Categories.Concretes;

namespace ToDoAssignment.Repository;

public static class RepositoryDependencies
{

    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();
        services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        return services;
    }
}
