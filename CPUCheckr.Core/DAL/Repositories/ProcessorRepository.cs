using CPUCheckr.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CPUCheckr.Core.DAL.Repositories;

internal sealed class ProcessorRepository : IProcessorRepository
{
    private readonly CpuCheckrDbContext _dbContext;
    private readonly DbSet<Processor> _processors;

    public ProcessorRepository(CpuCheckrDbContext dbContext)
    {
        _dbContext = dbContext;
        _processors = dbContext.Processors;
    }

    public async Task<Processor> GetAsync(Guid id)
        => await _processors.SingleOrDefaultAsync(x => x.Id == id);


    public async Task<ICollection<Processor>> GetAllAsync()
        => await _processors.ToListAsync();

    public async Task AddAsync(Processor processor)
    {
        await _processors.AddAsync(processor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Processor processor)
    {
        _processors.Update(processor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Processor processor)
    {
        _processors.Remove(processor);
        await _dbContext.SaveChangesAsync();
    }
}