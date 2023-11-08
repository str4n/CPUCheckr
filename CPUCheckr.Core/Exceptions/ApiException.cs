namespace CPUCheckr.Core.Exceptions;

internal abstract class ApiException : Exception
{
    protected ApiException(string message) : base(message)
    {
    }
}