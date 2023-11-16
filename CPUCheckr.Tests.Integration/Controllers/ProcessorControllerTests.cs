using System.Collections;
using System.Net;
using System.Net.Http.Json;
using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;
using FluentAssertions;
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
        _testDatabase.DbContext.Processors.Add(
            Processor.Create(Guid.Parse(Id), "intel", "i7-9700", 8, "4.8GHz",
            "Socket 1200", 1200));
        _testDatabase.DbContext.SaveChanges();
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

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}