using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CPUCheckr.Tests.Integration;

// ReSharper disable once InconsistentNaming
internal sealed class CPUCheckrTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public CPUCheckrTestApp()
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
        }).CreateClient();
    }
    
}