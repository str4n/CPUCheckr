using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Domain.ValueObjects;

internal sealed record Model
{
    public string Value { get; }

    public Model(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyModelException();
        }

        Value = value;
    }
    
    public static implicit operator string(Model model) => model.Value;
    public static implicit operator Model(string model) => new(model);
}