using CPUCheckr.Core.Domain.Entities;

namespace CPUCheckr.Core.DAL.Repositories;

internal interface IProcessorRepository
{
    Task<Processor> GetAsync(Guid id);
    Task<ICollection<Processor>> GetAllAsync(Guid id);
    Task AddAsync(Processor processor);
    Task UpdateAsync(Processor processor);
    Task DeleteAsync(Processor processor);
}