using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Exceptions;

internal sealed class ProcessorNotFoundException : NotFoundException
{
    public ProcessorNotFoundException(Guid id) : base($"Processor with id: {id} was not found.")
    {
    }
}