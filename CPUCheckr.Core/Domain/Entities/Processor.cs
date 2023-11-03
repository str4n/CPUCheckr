using CPUCheckr.Core.Domain.ValueObjects;

namespace CPUCheckr.Core.Domain.Entities;

internal sealed class Processor
{
    public Guid Id { get; private set; }
    public Manufacturer Manufacturer { get; private set; }
    public Model Model { get; private set; }
    public Cores Cores { get; private set; }
    public ClockRate ClockRate { get; private set; }
    public Socket Socket { get; private set; }

    public Processor(Guid id, Manufacturer manufacturer, Model model, Cores cores, ClockRate clockRate, Socket socket)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Manufacturer = manufacturer;
        Model = model;
        Cores = cores;
        ClockRate = clockRate;
        Socket = socket;
    }

    public static Processor Create(Guid id, Manufacturer manufacturer, Model model, Cores cores, ClockRate clockRate,
        Socket socket)
        => new(id, manufacturer, model, cores, clockRate, socket);

    public void EditManufacturer(Manufacturer manufacturer) => Manufacturer = manufacturer;
    public void EditModel(Model model) => Model = model;
    public void EditCores(Cores cores) => Cores = cores;
    public void EditClockRate(ClockRate clockRate) => ClockRate = clockRate;
    public void EditSocket(Socket socket) => Socket = socket;
}