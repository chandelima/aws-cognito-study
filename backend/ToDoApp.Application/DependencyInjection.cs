using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Interfaces.AuthService;
using ToDoApp.Application.Interfaces.ToDoService;
using ToDoApp.Application.Services.AuthService;
using ToDoApp.Application.Services.ToDoService;

namespace ToDoApp.Application;
public static class DependencyInjection
{
    private static void InjectDependencies(IServiceCollection services)
    {
        // Auth services
        services.AddScoped<IGetTokenFromAuthorizationCodeService, GetTokenFromAuthorizationCodeService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IRevokeTokenService, RevokeTokenService>();

        // ToDo services
        services.AddScoped(typeof(ValidateToDoService));
        services.AddScoped(typeof(ConvertToDoService));
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
