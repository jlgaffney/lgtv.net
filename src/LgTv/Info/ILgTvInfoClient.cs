using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Info
{
    public interface ILgTvInfoClient
    {
        Task<DateTime> GetCurrentTime();

        Task<SystemInformation> GetSystemInfo();

        Task<SoftwareInformation> GetSoftwareInfo();

        Task<ConnectionInformation> GetConnectionInfo();

        Task<IEnumerable<Service>> GetServices();
    }
}
