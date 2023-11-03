namespace CPUCheckr.Core.DTO;

public record ProcessorDto(Guid Id, string Manufacturer, string Model, int Cores, string ClockRate, string Socket, double Price);