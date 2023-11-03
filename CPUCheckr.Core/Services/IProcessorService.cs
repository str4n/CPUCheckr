using CPUCheckr.Core.DTO;

namespace CPUCheckr.Core.Services;

public interface IProcessorService
{
    Task<ProcessorDto> GetAsync(Guid id);
    Task<ICollection<ProcessorDto>> GetAllByManufacturerAsync(string manufacturer);
    Task<ICollection<ProcessorDto>> GetAllByCoresAsync(int cores);
    Task<ICollection<ProcessorDto>> GetAllAsync();
    Task AddAsync(ProcessorDto dto);
    Task UpdateAsync(Guid id, ProcessorDto dto);
    Task UpdatePriceAsync(Guid id, double price);
    Task DeleteAsync(Guid id);
}