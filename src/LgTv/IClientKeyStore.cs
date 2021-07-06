using System.Threading.Tasks;

namespace LgTv
{
    public interface IClientKeyStore
    {
        Task<string> GetClientKey();

        Task SetClientKey(string key);
    }
}
