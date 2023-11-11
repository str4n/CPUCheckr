namespace CPUCheckr.Core.CQRS.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
}