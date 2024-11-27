using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoAssignment.Service.Authentications.Services.Abstracts;
using ToDoAssignment.Service.Authentications.Services.Concretes;
using ToDoAssignment.Service.Categories.Abstracts;
using ToDoAssignment.Service.Categories.Concretes;
using ToDoAssignment.Service.JWT.Services.Abstracts;
using ToDoAssignment.Service.JWT.Services.Concretes;
using ToDoAssignment.Service.Roles.Services.Abstracts;
using ToDoAssignment.Service.Roles.Services.Concretes;
using ToDoAssignment.Service.Rules;
using ToDoAssignment.Service.Services.Users.Abstracts;
using ToDoAssignment.Service.Services.Users.Concretes;
using ToDoAssignment.Service.Todos.Abstracts;
using ToDoAssignment.Service.Todos.Concretes;

namespace ToDoAssignment.Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, EfCategoryService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IToDoService, EfToDoService>();
        services.AddScoped<IUserService, EfUserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<CategoryBusinessRules>();
        services.AddScoped<ToDoBusinessRules>();
     
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
