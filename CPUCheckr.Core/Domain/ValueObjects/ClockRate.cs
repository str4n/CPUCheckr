using System.Text.RegularExpressions;
using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Domain.ValueObjects;

internal sealed record ClockRate
{
    private static readonly Regex Regex = new(@"^\d+(\.\d+)?GHz$");
    
    public string Value { get; }

    public ClockRate(string value)
    {
        if (!Regex.IsMatch(value))
        {
            throw new InvalidClockRateException();
        }

        Value = value;
    }
    
    public static implicit operator string(ClockRate clockRate) => clockRate.Value;
    public static implicit operator ClockRate(string clockRate) => new(clockRate);
}