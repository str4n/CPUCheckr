using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Domain.ValueObjects;

internal sealed record Cores
{
    public int Value { get; }

    public Cores(int value)
    {
        if (value is < 1 or > 999)
        {
            throw new InvalidCoresAmountException();
        }

        Value = value;
    }
    
    public static implicit operator int(Cores cores) => cores.Value;
    public static implicit operator Cores(int cores) => new(cores);
}