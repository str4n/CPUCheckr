using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Domain.ValueObjects;

internal sealed record Manufacturer
{
    private static readonly string[] AvailableManufactures = { "intel", "qualcomm", "arm", "amd", "renesas" };
    
    public string Value { get; }

    public Manufacturer(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidManufacturerException();
        }

        if (!AvailableManufactures.Contains(value.ToLowerInvariant()))
        {
            throw new InvalidManufacturerException();
        }

        Value = value;
    }

    public static implicit operator string(Manufacturer manufacturer) => manufacturer.Value;
    public static implicit operator Manufacturer(string manufacturer) => new(manufacturer);
}