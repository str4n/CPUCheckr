using CPUCheckr.Core.DAL;
using Xunit;

namespace CPUCheckr.Tests.Integration.Controllers;

public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; } 
    
    protected ControllerTests(OptionsProvider optionsProvider)
    {
        var app = new CPUCheckrTestApp();
        Client = app.Client;
    }
}