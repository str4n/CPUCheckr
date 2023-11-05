using CPUCheckr.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CPUCheckr.Core.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<CpuCheckrDbContext>();
        
        await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);

        var processors = await dbContext.Processors.ToListAsync(cancellationToken: cancellationToken);

        if (processors.Any()) return;

        processors = new List<Processor>()
        {
            Processor.Create(Guid.NewGuid(), "intel", "i7-10700", 8, "4.8GHz", "Socket 1200", 1200),
            Processor.Create(Guid.NewGuid(), "intel", "i9-11900k", 8, "5.3GHz", "Socket 1200", 1700),
            Processor.Create(Guid.NewGuid(), "amd", "Ryzen 5 5600G", 6, "4.4GHz", "Socket AM4", 579),
            Processor.Create(Guid.NewGuid(), "amd", "Ryzen 9 7900", 12, "5.4GHz", "Socket AM5", 2029)
        };
            
        await dbContext.Processors.AddRangeAsync(processors, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}