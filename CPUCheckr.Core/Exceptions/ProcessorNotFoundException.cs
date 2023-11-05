using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Exceptions;

internal sealed class ProcessorNotFoundException : ApiException
{
    public ProcessorNotFoundException(Guid id) : base($"Processor with id: {id} was not found.")
    {
    }
}