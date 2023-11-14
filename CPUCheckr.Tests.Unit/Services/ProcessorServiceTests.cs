using CPUCheckr.Core.DAL.Repositories;
using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;
using CPUCheckr.Core.Mappings;
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

        var processor = await _processorService.GetAsync(id);

        processor.Should().NotBeNull();
        processor.Id.Should().Be(id);
        processor.Should().BeOfType<ProcessorDto>();
    }

    [Theory]
    [InlineData("intel", 8, "Socket 1200", 2)]
    [InlineData("amd", 8, "Socket 1200", 0)]
    [InlineData("amd", default(int), "Socket AM4", 1)]
    [InlineData("amd", default(int), "Socket AM5", 1)]
    public async void given_valid_sort_dto_returns_collection_of_all_processors(string manufacturer, int cores, string socket, int expectedCount)
    {
        var sortBy = new QueryDto(manufacturer, default, cores, socket);
        
        var processors = await _processorService.GetAllAsync(sortBy);

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

    [Theory]
    [InlineData( "amd", "test", 8, "7.8GHz", "Socket 1200", 1600)]
    [InlineData( "intel", "Test", 10, "4.8GHz", "Socket 2137", 1200)]
    public async void given_valid_processor_dto_should_add_processor_to_repository(string manufacturer, string model, int cores, string clockRate, string socket, double price)
    {
        var id = Guid.NewGuid();
        var processor = Processor.Create(id, manufacturer, model, cores, clockRate, socket, price);

        await _processorService.AddAsync(processor.AsDto());

        var processors = await _processorRepository.GetAllAsync();

        processors.Should().Contain(x => x.Id == id);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    [InlineData("00000000-0000-0000-0000-000000000002")]
    public async void given_valid_id_should_delete_processor_from_repository(string id)
    {
        var guid = Guid.Parse(id);
        await _processorService.DeleteAsync(guid);

        var processors = await _processorRepository.GetAllAsync();

        processors.Should().NotContain(x => x.Id == guid);
    }

    [Fact]
    public async void given_valid_price_updating_price_should_succeed()
    {
        var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var price = new Random().NextDouble() * 2000;

        await _processorService.UpdatePriceAsync(id, price);

        var processor = await _processorRepository.GetAsync(id);

        processor.Price.Value.Should().Be(price);
    }

    private readonly IProcessorRepository _processorRepository;
    private readonly IProcessorService _processorService;

    public ProcessorServiceTests()
    {
        _processorRepository = new InMemoryProcessorRepository();
        _processorService = new ProcessorService(_processorRepository);
    }
}