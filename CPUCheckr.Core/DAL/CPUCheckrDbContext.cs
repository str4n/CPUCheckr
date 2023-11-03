using CPUCheckr.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CPUCheckr.Core.DAL;

internal sealed class CpuCheckrDbContext : DbContext
{
    public DbSet<Processor> Processors { get; set; }

    public CpuCheckrDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}