using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CPUCheckr.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        return services;
    }

    public static WebApplication UseCore(this WebApplication app)
    {
        app.MapControllers();
        
        app.MapGet("/", () => "Hello World!");

        return app;
    }
}