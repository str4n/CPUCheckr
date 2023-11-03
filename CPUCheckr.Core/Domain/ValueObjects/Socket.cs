using CPUCheckr.Core.Domain.Exceptions;

namespace CPUCheckr.Core.Domain.ValueObjects;

internal sealed record Socket
{
    public string Value { get; }

    public Socket(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptySocketException();
        }

        Value = value;
    }
    
    public static implicit operator string(Socket socket) => socket.Value;
    public static implicit operator Socket(string socket) => new(socket);
}