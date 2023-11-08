using CPUCheckr.Core.Exceptions;

namespace CPUCheckr.Core.Domain.Exceptions;

internal sealed class InvalidClockRateException : ApiException
{
    public InvalidClockRateException() : base("Clock rate must be in format: { *GHz }")
    {
    }
}