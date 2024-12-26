using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.Infra.Services;

public static class MigrationService
{
    public static void ApplyAll(IServiceScope appScope)
    {
        using (var scope = appScope)
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
