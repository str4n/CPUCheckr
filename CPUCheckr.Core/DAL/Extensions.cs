using CPUCheckr.Core.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CPUCheckr.Core.DAL;

internal static class Extensions
{
    private const string SectionName = "MariaDb";
    
    public static IServiceCollection AddMariaDb(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<MariaDbOptions>(section);

        var options = configuration.GetOptions<MariaDbOptions>(SectionName);

        services.AddDbContext<CpuCheckrDbContext>(x =>
            x.UseMySql(options.ConnectionString, new MySqlServerVersion(new Version(11, 1))));

        services.AddScoped<IProcessorRepository, ProcessorRepository>();

        return services;
    }
}