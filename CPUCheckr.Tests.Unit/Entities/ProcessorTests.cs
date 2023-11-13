using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.Domain.Exceptions;
using CPUCheckr.Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace CPUCheckr.Tests.Entities;

public class ProcessorTests
{
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", 8, "4.8GHz", "Socket 1200", 1200)]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i9-11900k", 8, "5.3GHz", "Socket 1200", 1700)]
    [InlineData("00000000-0000-0000-0000-000000000001", "amd", "Ryzen 5 5600G", 6, "4.4GHz", "Socket AM4", 579)]
    [InlineData("00000000-0000-0000-0000-000000000001", "amd", "Ryzen 9 7900", 12, "5.4GHz", "Socket AM5", 2029)]
    public void given_valid_processor_properties_create_should_succeed(string id,string manufacturer, string model, int cores, string clockRate, string socket, double price)
    {
        var act = () => Processor.Create(Guid.Parse(id), manufacturer, model, cores, clockRate, socket, price);

        act.Should().NotThrow<ApiException>();
        act.Invoke().Should().NotBeNull();
    }
    
    
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", 0, "4.8GHz", "Socket 1200", 1200)]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", -2, "4.8GHz", "Socket 1200", 1200)]
    public void given_invalid_processor_cores_should_throw_exception(string id, string manufacturer, string model,
        int cores, string clockRate, string socket, double price)
    {
        var act = () => Processor.Create(Guid.Parse(id), manufacturer, model, cores, clockRate, socket, price);
        
        act.Should().Throw<InvalidCoresAmountException>();
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", 8, "4,8GHz", "Socket 1200", 1200)]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", 8, "4.8Ghz", "Socket 1200", 1200)]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", 8, "", "Socket 1200", 1200)]
    [InlineData("00000000-0000-0000-0000-000000000001", "intel", "i7-10700", 8, "4.8", "Socket 1200", 1200)]
    public void given_invalid_processor_clockrate_should_throw_exception(string id, string manufacturer, string model,
        int cores, string clockRate, string socket, double price)
    {
        var act = () => Processor.Create(Guid.Parse(id), manufacturer, model, cores, clockRate, socket, price);

        act.Should().Throw<InvalidClockRateException>();
    }
}