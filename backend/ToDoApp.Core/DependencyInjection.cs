using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.Services;

namespace ToDoApp.Application;
public static class DependencyInjection
{
    private static void InjectDependencies(IServiceCollection services)
    {
        services.AddScoped<IGetRequestTokenDataService, GetRequestTokenDataService>();
    }

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        InjectDependencies(services);

        return services;
    }
}
