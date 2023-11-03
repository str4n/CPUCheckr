using CPUCheckr.Core.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CPUCheckr.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ExceptionMiddleware>();

        return services;
    }

    public static WebApplication UseCore(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
            
        app.MapGet("/", () => "Hello World!");
        
        app.MapControllers();

        return app;
    }
}