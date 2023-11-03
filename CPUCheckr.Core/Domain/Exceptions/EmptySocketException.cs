namespace CPUCheckr.Core.Domain.Exceptions;

internal sealed class EmptySocketException : ApiException
{
    public EmptySocketException() : base("Socket cannot be empty.")
    {
    }
}