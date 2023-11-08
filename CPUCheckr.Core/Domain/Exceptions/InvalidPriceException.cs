using CPUCheckr.Core.Exceptions;

namespace CPUCheckr.Core.Domain.Exceptions;

internal sealed class InvalidPriceException : ApiException
{
    public InvalidPriceException() : base("Processor price cannot be negative.")
    {
    }
}