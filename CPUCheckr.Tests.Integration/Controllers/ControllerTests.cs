using Microsoft.AspNetCore.Hosting;

namespace CPUCheckr.Tests.Integration.Controllers;

public abstract class ControllerTests
{
    protected HttpClient Client { get; } 
    
    protected ControllerTests()
    {
        var app = new CPUCheckrTestApp();
        Client = app.Client;
    }
}