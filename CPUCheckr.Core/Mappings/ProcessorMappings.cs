using CPUCheckr.Core.Domain.Entities;
using CPUCheckr.Core.DTO;

namespace CPUCheckr.Core.Mappings;

internal static class ProcessorMappings
{
    public static ProcessorDto AsDto(this Processor processor)
        => new(processor.Id, processor.Manufacturer, processor.Model, processor.Cores, processor.ClockRate,
            processor.Socket, processor.Price);

    public static Processor ToEntity(this ProcessorDto dto)
        => Processor.Create(dto.Id, dto.Manufacturer, dto.Model, dto.Cores, dto.ClockRate, dto.Socket, dto.Price);
}