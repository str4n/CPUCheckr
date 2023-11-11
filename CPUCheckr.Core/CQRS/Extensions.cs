using CPUCheckr.Core.CQRS.Abstractions.Commands;
using CPUCheckr.Core.CQRS.Abstractions.Queries;
using CPUCheckr.Core.CQRS.Commands;
using CPUCheckr.Core.CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CPUCheckr.Core.CQRS;

internal static class Extensions
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        services
            .AddSingleton<ICommandDispatcher, CommandDispatcher>()
            .AddSingleton<IQueryDispatcher, QueryDispatcher>();
        
        return services;
    }
}