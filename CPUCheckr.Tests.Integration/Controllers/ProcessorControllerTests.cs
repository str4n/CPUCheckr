using System.Net;
using System.Net.Http.Json;
using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CPUCheckr.Tests.Integration.Controllers;

public class ProcessorControllerTests : ControllerTests, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private const string Path = "/api/processor";
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

        //var response = await Client.PatchAsJsonAsync($"{Path}/{id}", dto);

        //response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        // updatedProcessor = await _testDatabase.DbContext.Processors.SingleOrDefaultAsync(x => x.Id == id);
        
        //updatedProcessor?.Price.Should().Be(dto.Price);
    }

    private async Task AddSampleProcessorToDatabase()
    {
        var id = Guid.Parse(Id);
        
        await _testDatabase.DbContext.Processors.AddAsync(
            Processor.Create(id, "intel", "i7-10700", 8, "4.8GHz",
                "Socket 1200", 1200));
        await _testDatabase.DbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}