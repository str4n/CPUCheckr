﻿using CPUCheckr.Core.CQRS.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CPUCheckr.Core.CQRS.Queries;

internal sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>
    {
        if (query is null)
        {
            return default;
        }

        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.HandleAsync(query);
    }
}