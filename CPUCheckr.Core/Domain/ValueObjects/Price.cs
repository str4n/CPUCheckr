using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Domain.ValueObjects;

internal sealed record Price
{
    public double Value { get; }

    public Price(double value)
    {
        if (value < 1)
        {
            throw new InvalidPriceException();
        }

        Value = value;
    }

    public static implicit operator double(Price price) => price.Value;
    public static implicit operator Price(double price) => new(price);
}