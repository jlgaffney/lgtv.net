namespace LgTv.Scanning;

public interface ILgTvScanner
{
    Task<IEnumerable<Device>> GetDevices();
}
