using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infra.DataContext;
using ToDoApp.Infra.Migrations;
using ToDoApp.Infra.Repositories;

namespace ToDoApp.Infra;
public static class DependencyInjection
{
    private static void SetupDatabase(IServiceCollection services)
    {
        var connectionString = "Data Source=ToDoApp.db";

        services.AddDbContext<ToDoAppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(cfg => cfg
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(CreateTabelaVersionInfoMetadata).Assembly)
                .For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .AddTransient<IVersionTableMetaData, CreateTabelaVersionInfoMetadata>();
    }

    private static void InjectDependencies(IServiceCollection services)
    {
        services.AddScoped<IToDoRepository, ToDoRepository>();
    }

    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        SetupDatabase(services);
        InjectDependencies(services);

        return services;
    }
}
