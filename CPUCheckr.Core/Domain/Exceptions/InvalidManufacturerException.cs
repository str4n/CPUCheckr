using CPUCheckr.Core.Exceptions;

namespace CPUCheckr.Core.Domain.Exceptions;

internal sealed class InvalidManufacturerException : ApiException
{
    public InvalidManufacturerException() : base("Manufacturer name is invalid. Correct manufacturers are { intel, qualcomm, arm, amd, renesas }")
    {
    }
}