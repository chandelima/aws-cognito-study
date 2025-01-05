using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infra.Data.DataContext;
using ToDoApp.Infra.Data.Migrations;
using ToDoApp.Infra.Data.Repositories;

namespace ToDoApp.Infra.Data;
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

    public static IServiceCollection AddInfraData(this IServiceCollection services)
    {
        SetupDatabase(services);
        InjectDependencies(services);

        return services;
    }
}
