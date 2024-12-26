using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application;
using ToDoApp.Infra;

namespace ToDoApp.IoC;
public static class DependencyInjection
{
    public static void SetupToDoApp(this IServiceCollection services)
    {
        services
            .AddInfra()
            .AddApplication();
    }
}
