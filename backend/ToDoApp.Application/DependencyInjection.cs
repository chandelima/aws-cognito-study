using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;

namespace ToDoApp.Application;
public static class DependencyInjection
{
    private static void InjectDependencies(IServiceCollection services)
    {
        services.AddScoped(typeof(ValidateToDoService));
        services.AddScoped<ICreateToDoService, CreateToDoService>();
        services.AddScoped<IGetToDoService, GetToDoService>();
        services.AddScoped<IUpdateToDoService, UpdateToDoService>();
        services.AddScoped<IDeleteToDoService, DeleteToDoService>();
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        InjectDependencies(services);

        return services;
    }
}
