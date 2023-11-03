namespace CPUCheckr.Core.Domain.Exceptions;

internal class ApiException : Exception
{
    protected ApiException(string message) : base(message)
    {
    }
}