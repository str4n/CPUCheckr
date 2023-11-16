using System.Collections;
using System.Net;
using System.Net.Http.Json;
using CPUCheckr.Core.DTO;
using FluentAssertions;
using Xunit;

namespace CPUCheckr.Tests.Integration.Controllers;

public class ProcessorControllerTests : ControllerTests
{
    private const string Path = "/api/processor";
    
    [Fact]
    public async Task get_processors_should_return_200_status_code()
    {
        var response = await Client.GetAsync(Path);

        var content = await response.Content.ReadFromJsonAsync<ICollection<ProcessorDto>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeEmpty();
    }
}