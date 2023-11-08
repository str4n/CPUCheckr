namespace CPUCheckr.Core.Exceptions;

internal abstract class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(message)
    {
    }
}