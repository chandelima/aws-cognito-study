using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application;
using ToDoApp.Infra.Data;
using ToDoApp.Infra.External;

namespace ToDoApp.IoC;
public static class DependencyInjection
{
    public static void SetupToDoApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfraData()
                .AddInfraExternal(configuration)
                .AddApplication()
                .AddCore();
    }
}
