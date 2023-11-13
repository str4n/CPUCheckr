using CPUCheckr.Core.DAL.Repositories;
using CPUCheckr.Core.Domain.Entities;

namespace CPUCheckr.Tests.Repositories;

internal class InMemoryProcessorRepository : IProcessorRepository
{
    private readonly List<Processor> _processorRepository = new()
    {
        Processor.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), "intel", "i7-10700", 8, "4.8GHz", "Socket 1200", 1200),
        Processor.Create(Guid.Parse("00000000-0000-0000-0000-000000000002"), "intel", "i9-11900k", 8, "5.3GHz", "Socket 1200", 1700),
        Processor.Create(Guid.Parse("00000000-0000-0000-0000-000000000003"), "amd", "Ryzen 5 5600G", 6, "4.4GHz", "Socket AM4", 579),
        Processor.Create(Guid.Parse("00000000-0000-0000-0000-000000000004"), "amd", "Ryzen 9 7900", 12, "5.4GHz", "Socket AM5", 2029)
    };

    public Task<Processor> GetAsync(Guid id)
        => Task.FromResult(_processorRepository.SingleOrDefault(x => x.Id == id))!;

    public  Task<ICollection<Processor>> GetAllAsync()
        => Task.FromResult<ICollection<Processor>>(_processorRepository);

    public Task AddAsync(Processor processor)
    {
        _processorRepository.Add(processor);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Processor processor)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Processor processor)
    {
        _processorRepository.Remove(processor);

        return Task.CompletedTask;
    }
}