namespace CPUCheckr.Core.Domain.Exceptions;

internal sealed class EmptyModelException : ApiException
{
    public EmptyModelException() : base("Processor model cannot be empty.")
    {
    }
}