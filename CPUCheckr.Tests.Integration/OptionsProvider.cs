using CPUCheckr.Core;
using Microsoft.Extensions.Configuration;

namespace CPUCheckr.Tests.Integration;

public sealed class OptionsProvider
{
    private readonly IConfiguration _configuration = GetConfigurationRoot();

    public TOptions GetOptions<TOptions>(string sectionName) where TOptions : class, new()
        => _configuration.GetOptions<TOptions>(sectionName);

    private static IConfiguration GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", true)
            .AddEnvironmentVariables()
            .Build();
}