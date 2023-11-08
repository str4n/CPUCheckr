using CPUCheckr.Core.Exceptions;

namespace CPUCheckr.Core.Domain.Exceptions;

internal sealed class InvalidCoresAmountException : ApiException
{
    public InvalidCoresAmountException() : base("Processor cores count must be in (1-999).")
    {
    }
}