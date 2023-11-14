using CPUCheckr.Core.DAL.Repositories;
using CPUCheckr.Core.DTO;
using CPUCheckr.Core.Exceptions;
using CPUCheckr.Core.Mappings;

namespace CPUCheckr.Core.Services;

internal sealed class ProcessorService : IProcessorService
{
    private readonly IProcessorRepository _processorRepository;

    public ProcessorService(IProcessorRepository processorRepository)
    {
        _processorRepository = processorRepository;
    }

    public async Task<ProcessorDto> GetAsync(Guid id)
    {
        var processor = await _processorRepository.GetAsync(id);

        if (processor is null)
        {
            throw new ProcessorNotFoundException(id);
        }

        return processor.AsDto();
    }

    public async Task<ICollection<ProcessorDto>> GetAllAsync(QueryDto query)
        => (await _processorRepository.GetAllAsync())
            .Where(x => query.Manufacturer is null || string.Equals(x.Manufacturer.Value, query.Manufacturer, StringComparison.InvariantCultureIgnoreCase))
            .Where(x => query.Model is null || string.Equals(x.Model.Value, query.Model, StringComparison.InvariantCultureIgnoreCase))
            .Where(x => query.Cores <= default(int) || x.Cores == query.Cores)
            .Where(x => query.Socket is null || string.Equals(x.Socket.Value, query.Socket, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.AsDto())
            .ToList();

    public async Task AddAsync(ProcessorDto dto)
    {
        if (dto is null)
        {
            throw new DtoNullException();
        }

        await _processorRepository.AddAsync(dto.ToEntity());
    }

    public async Task UpdateAsync(Guid id, ProcessorDto dto)
    {
        var processor = await _processorRepository.GetAsync(id);
        
        if (processor is null)
        {
            throw new ProcessorNotFoundException(id);
        }

        if (dto.Manufacturer is not null)
        {
            processor.EditManufacturer(dto.Manufacturer);
        }
        
        if (dto.Model is not null)
        {
            processor.EditModel(dto.Model);
        }
        
        if (dto.Cores is not default(int))
        {
            processor.EditCores(dto.Cores);
        }
        
        if (dto.ClockRate is not null)
        {
            processor.EditClockRate(dto.ClockRate);
        }
        
        if (dto.Socket is not null)
        {
            processor.EditSocket(dto.Socket);
        }

        if (dto.Price is not default(double))
        {
            processor.EditPrice(dto.Price);
        }

        await _processorRepository.UpdateAsync(processor);
    }

    public async Task UpdatePriceAsync(Guid id, double price)
    {
        var processor = await _processorRepository.GetAsync(id);
        
        if (processor is null)
        {
            throw new ProcessorNotFoundException(id);
        }
        
        processor.EditPrice(price);

        await _processorRepository.UpdateAsync(processor);
    }

    public async Task DeleteAsync(Guid id)
    {
        var processor = await _processorRepository.GetAsync(id);

        if (processor is null)
        {
            throw new ProcessorNotFoundException(id);
        }

        await _processorRepository.DeleteAsync(processor);
    }
}