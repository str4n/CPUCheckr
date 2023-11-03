using CPUCheckr.Core.DAL;
using CPUCheckr.Core.Exceptions.Middleware;
using CPUCheckr.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CPUCheckr.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddScoped<ExceptionMiddleware>();

        services.AddMariaDb(configuration);

        services.AddScoped<IProcessorService, ProcessorService>();

        return services;
    }

    public static WebApplication UseCore(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllers();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}