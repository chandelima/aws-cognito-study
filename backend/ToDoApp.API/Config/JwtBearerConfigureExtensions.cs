using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace ToDoApp.API.Config;

public class JwtBearerConfigureExtensions(IConfiguration configuration) : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "JwtSettings";
    
    public void Configure(JwtBearerOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);

        options.TokenValidationParameters.ClockSkew = TimeSpan.FromMilliseconds(0);

    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}
