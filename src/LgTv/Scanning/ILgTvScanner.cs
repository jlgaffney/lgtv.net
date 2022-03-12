using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Scanning
{
    public interface ILgTvScanner
    {
        Task<IEnumerable<Device>> GetDevices();
    }
}
