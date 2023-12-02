namespace LgTv.Scanning;

public class Device(
    string id,
    string name,
    string ipAddress)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    public string IpAddress { get; } = ipAddress;
}
