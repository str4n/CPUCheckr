using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;
using CPUCheckr.Core.Mappings;
using FluentAssertions;
using Xunit;

namespace CPUCheckr.Tests.Mappings;

public class MappingsTests
{
    [Fact]
    public void given_valid_dto_should_map_to_object()
    {
        var id = Guid.NewGuid();
        var dto = new ProcessorDto(id, "intel", "test", 8, "8GHz", "test", 1900);
        var validProcessor = Processor.Create(id, "intel", "test", 8, "8GHz", "test", 1900);

        var mappedProcessor = dto.ToEntity();

        mappedProcessor.Should().BeEquivalentTo(validProcessor);
    }
    
    [Fact]
    public void given_valid_object_should_map_to_dto()
    {
        var id = Guid.NewGuid();
        var processor = Processor.Create(id, "intel", "test", 8, "8GHz", "test", 1900);
        var validDto = new ProcessorDto(id, "intel", "test", 8, "8GHz", "test", 1900);

        var mappedDto = processor.AsDto();

        mappedDto.Should().BeEquivalentTo(validDto);
    }
}