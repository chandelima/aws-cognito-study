namespace ToDoApp.API.Config;

public static class CorsConfigExtensions
{
    public static void SetupCors(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Development",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            var origins = configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>();

            options.AddPolicy("Production",
                policy => policy
                    .WithOrigins(origins!)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
}
