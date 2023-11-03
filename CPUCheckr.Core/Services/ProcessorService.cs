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
        => (await _processorRepository.GetAsync(id)).AsDto();

    public async Task<ICollection<ProcessorDto>> GetAllByManufacturerAsync(string manufacturer)
        => (await _processorRepository.GetAllAsync())
            .Where(x => x.Manufacturer == manufacturer)
            .Select(x => x.AsDto())
            .ToList();

    public async Task<ICollection<ProcessorDto>> GetAllByCoresAsync(int cores)
        => (await _processorRepository.GetAllAsync())
            .Where(x => x.Cores == cores)
            .Select(x => x.AsDto())
            .ToList();

    public async Task<ICollection<ProcessorDto>> GetAllAsync()
        => (await _processorRepository.GetAllAsync())
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

        if (dto.Manufacturer is not null)
        {
            processor.EditManufacturer(dto.Manufacturer);
        }
        
        if (dto.Model is not null)
        {
            processor.EditModel(dto.Model);
        }
        
        if (dto.Cores is not 0)
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

        if (dto.Price is not 0)
        {
            processor.EditPrice(dto.Price);
        }

        await _processorRepository.UpdateAsync(processor);
    }

    public async Task UpdatePriceAsync(Guid id, double price)
    {
        var processor = await _processorRepository.GetAsync(id);
        processor.EditPrice(price);

        await _processorRepository.UpdateAsync(processor);
    }

    public async Task DeleteAsync(Guid id)
    {
        var processor = await _processorRepository.GetAsync(id);

        await _processorRepository.DeleteAsync(processor);
    }
}