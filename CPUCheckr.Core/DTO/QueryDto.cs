namespace CPUCheckr.Core.DTO;

public sealed record QueryDto(string Manufacturer, string Model, int Cores, string Socket);