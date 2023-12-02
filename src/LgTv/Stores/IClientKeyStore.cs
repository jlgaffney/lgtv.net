namespace LgTv.Stores;

public interface IClientKeyStore
{
    Task<string> GetClientKey(string ipAddress);

    Task SetClientKey(string ipAddress, string key);
}
