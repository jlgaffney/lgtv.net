namespace LgTv.Features.Info;

public interface ITvInfoClient
{
    Task<DateTime> GetCurrentTime();

    Task<SystemInformation> GetSystemInfo();

    Task<SoftwareInformation> GetSoftwareInfo();

    Task<ConnectionInformation> GetConnectionInfo();

    Task<IEnumerable<Service>> GetServices();
}
