using System.Threading.Tasks;

namespace LgTv
{
    public interface IClientKeyStore
    {
        Task<string> GetClientKey(string ipAddress);

        Task SetClientKey(string ipAddress, string key);
    }
}
