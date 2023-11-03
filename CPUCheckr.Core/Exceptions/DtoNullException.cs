using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Exceptions;

internal sealed class DtoNullException : ApiException
{
    public DtoNullException() : base("Processor cannot be null.")
    {
    }
}