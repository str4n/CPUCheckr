using System.Net;
using System.Net.Http.Json;
using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;
using CPUCheckr.Core.Mappings;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CPUCheckr.Tests.Integration.Controllers;

public class ProcessorControllerTests : ControllerTests, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private const string Path = "api/processor";
    private const string Id = "00000000-0000-0000-0000-000000000001";
    
    public ProcessorControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    [Fact]
    public async Task get_processors_should_return_200_status_code()
    {
        var response = await Client.GetAsync(Path);

        var content = await response.Content.ReadFromJsonAsync<ICollection<ProcessorDto>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeEmpty();
    }
    
    [Theory]
    [InlineData("intel", 8)]
    [InlineData("amd", 6)]
    public async Task get_processors_with_filtering_should_return_200_status_code(string manufacturer, int cores)
    {
        var response = await Client.GetAsync($"{Path}?manufacturer={manufacturer}&cores={cores}");

        var content = await response.Content.ReadFromJsonAsync<ICollection<ProcessorDto>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content
            .Should().AllSatisfy(x => x.Manufacturer.Should().Be(manufacturer))
            .And.AllSatisfy(x => x.Cores.Should().Be(cores));
    }

    [Fact]
    public async Task get_processor_by_id_should_return_200_status_code_and_processor()
    {
        var id = Guid.Parse(Id);

        await AddSampleProcessorToDatabase();
        
        var response = await Client.GetAsync($"{Path}/{id}");

        var processor = await response.Content.ReadFromJsonAsync<ProcessorDto>();
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        processor.Should().NotBeNull();
        processor?.Id.Should().Be(id);
    }

    [Fact]
    public async Task post_processor_should_return_201_status_code()
    {
        var dto = new ProcessorDto(default, "intel", "i7-11700k",8,"5.0GHz","Socket 1200",1200);
        
        var response = await Client.PostAsJsonAsync($"{Path}/create", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }

    [Fact]
    public async Task update_processor_price_should_return_202_status_code()
    {
        var id = Guid.Parse(Id);
        var dto = new PriceDto(999);

        await AddSampleProcessorToDatabase();

        var response = await Client.PatchAsJsonAsync($"{Path}/{id}", dto);

        await ReloadEntry(id);
        
        // ReSharper disable once EntityFramework.NPlusOne.IncompleteDataQuery
        var editedProcessor = await _testDatabase.DbContext.Processors.FirstOrDefaultAsync(x => x.Id == id);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        // ReSharper disable once EntityFramework.NPlusOne.IncompleteDataUsage
        editedProcessor?.Price.Value.Should().Be(dto.Price);
    }

    [Fact]
    public async Task update_processor_should_return_202_status_code()
    {
        var id = Guid.Parse(Id);
        var dto = new ProcessorDto(id, "amd", "Ryzen 5 5600", 6, "4.7GHz", "Socket AM4", 700);

        await AddSampleProcessorToDatabase();

        var response = await Client.PutAsJsonAsync($"{Path}/{id}", dto);

        await ReloadEntry(id);
        
        var editedProcessor = await _testDatabase.DbContext.Processors.FirstOrDefaultAsync(x => x.Id == id);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        editedProcessor.Should().BeEquivalentTo(dto.ToEntity());
    }

    [Fact]
    public async Task delete_processor_should_return_204_status_code()
    {
        var id = Guid.Parse(Id);
        
        await AddSampleProcessorToDatabase();
        
        var response = await Client.DeleteAsync($"{Path}/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var deletedProcessor = await _testDatabase.DbContext.Processors.FirstOrDefaultAsync(x => x.Id == id);

        deletedProcessor.Should().BeNull();
    }

    private async Task AddSampleProcessorToDatabase()
    {
        var id = Guid.Parse(Id);
        
        await _testDatabase.DbContext.Processors.AddAsync(
            Processor.Create(id, "intel", "i7-10700", 8, "4.8GHz",
                "Socket 1200", 1200));
        await _testDatabase.DbContext.SaveChangesAsync();
    }

    private async Task ReloadEntry(Guid id)
    {
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        await _testDatabase.DbContext.Entry(_testDatabase.DbContext.Processors.FirstOrDefault(x => x.Id == id))
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            .ReloadAsync();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}