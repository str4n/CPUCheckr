using CPUCheckr.Core.DAL.Repositories;
using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;
using CPUCheckr.Core.Services;
using CPUCheckr.Tests.Repositories;
using FluentAssertions;
using Xunit;

namespace CPUCheckr.Tests.Services;

public class ProcessorServiceTests
{
    [Fact]
    public async void given_valid_id_returns_processor()
    {
        var id = Guid.Parse("00000000-0000-0000-0000-000000000001");

        var processor = await ProcessorService.GetAsync(id);

        processor.Should().NotBeNull();
        processor.Should().BeOfType<ProcessorDto>();
    }

    [Theory]
    [InlineData("intel", 8, "Socket 1200", 2)]
    [InlineData("amd", 8, "Socket 1200", 0)]
    [InlineData("amd", default(int), "Socket AM4", 1)]
    [InlineData("amd", default(int), "Socket AM5", 1)]
    public async void given_valid_sort_dto_returns_collection_of_all_processors(string manufacturer, int cores, string socket, int expectedCount)
    {
        var sortBy = new SortByDto(manufacturer, default, cores, socket);
        
        var processors = await ProcessorService.GetAllAsync(sortBy);

        processors.Should().NotBeNull().And.HaveCount(expectedCount);

        if (processors.Count <= default(int)) return;
        
        foreach (var processor in processors)
        {
            processor.Manufacturer?.Should().Be(manufacturer);

            if (processor.Cores <= default(int))
            {
                processor.Cores.Should().Be(cores);
            }

            processor.Socket?.Should().Be(socket);
        }
    }

    private static readonly IProcessorRepository ProcessorRepository = new InMemoryProcessorRepository();
    private static readonly IProcessorService ProcessorService = new ProcessorService(ProcessorRepository);
}