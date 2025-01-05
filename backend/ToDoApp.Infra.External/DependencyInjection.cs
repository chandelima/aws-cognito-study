using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Infra.External.Constants;
using ToDoApp.Infra.External.Entities;
using ToDoApp.Infra.External.Interfaces;
using ToDoApp.Infra.External.Services.AwsCognitoService;

namespace ToDoApp.Infra.External;
public static class DependencyInjection
{
    private static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGetTokenFromAuthCodeAwsCognitoService, GetTokenFromAuthCodeAWSCognitoService>();
        services.AddScoped<IGetTokenFromRefreshTokenAwsCognitoService, GetTokenFromRefreshTokenAwsCognitoService>();
        services.AddScoped<IRevokeTokenAwsCognitoService, RevokeTokenAwsCognitoService>();

        var keyAwsCognitoDomain = ConfigurationsConstants.AWS_COGNITO_DOMAIN_KEY;
        var uriAwsCognitoDomain = configuration.GetValue<string>(keyAwsCognitoDomain);

        services.AddHttpClient(keyAwsCognitoDomain, client =>
        {
            client.BaseAddress = new Uri(uriAwsCognitoDomain!);
        });
    }

    public static IServiceCollection AddInfraExternal(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AwsCognitoSettings>(configuration.GetSection("AwsCognitoSettings"));

        InjectDependencies(services, configuration);

        return services;
    }
}
