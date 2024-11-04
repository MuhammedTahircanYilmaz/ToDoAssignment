using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoAssignment.Service.Rules;
using ToDoAssignment.Service.Services.Categories.Abstracts;
using ToDoAssignment.Service.Services.Categories.Concretes;

namespace ToDoAssignment.Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, EfCategoryService>();
        services.AddScoped<CategoryBusinessRules>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
