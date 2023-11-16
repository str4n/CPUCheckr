using CPUCheckr.Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace CPUCheckr.Tests.Integration;

internal sealed class TestDatabase : IDisposable
{
    public CpuCheckrDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().GetOptions<MariaDbOptions>("MariaDb");
        DbContext = new CpuCheckrDbContext(new DbContextOptionsBuilder<CpuCheckrDbContext>()
            .UseMySql(options.ConnectionString, new MySqlServerVersion(new Version(11, 1))).Options);
    }

    public void Dispose()
    {
        DbContext.Processors.RemoveRange(DbContext.Processors);
        DbContext.SaveChanges();
        DbContext.Dispose();
    }
}