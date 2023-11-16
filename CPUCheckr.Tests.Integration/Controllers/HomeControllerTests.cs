using System.Net;
using FluentAssertions;
using Xunit;

namespace CPUCheckr.Tests.Integration.Controllers;

public class HomeControllerTests : ControllerTests
{
    public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }
    
    
    [Fact]
    public async Task get_base_endpoint_should_return_200_status_code_and_api_name()
    {
        var response = await Client.GetAsync("/api/home");

        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Be("CpuCheckr API");
    }
}