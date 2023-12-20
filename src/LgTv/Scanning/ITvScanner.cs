namespace LgTv.Scanning;

public interface ITvScanner
{
    Task<IEnumerable<Device>> GetDevices();
}
