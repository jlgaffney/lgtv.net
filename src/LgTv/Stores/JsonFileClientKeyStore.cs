using Newtonsoft.Json.Linq;

namespace LgTv.Stores;

public class JsonFileClientKeyStore(string clientKeyStoreJsonFilePath) : IClientKeyStore
{
    private const string DefaultJsonContents = "{\n}";
    private const string ClientKeyJsonPropertyName = "client-key";

    public async Task<string> GetClientKey(string ipAddress)
    {
        await Task.CompletedTask;

        if (!File.Exists(clientKeyStoreJsonFilePath))
        {
            return null;
        }

        var clientKeyStoreFileContents = File.ReadAllText(clientKeyStoreJsonFilePath);

        if (string.IsNullOrWhiteSpace(clientKeyStoreFileContents))
        {
            clientKeyStoreFileContents = DefaultJsonContents;
        }

        var json = JObject.Parse(clientKeyStoreFileContents);

        var key = json[ipAddress]?[ClientKeyJsonPropertyName]?.ToObject<string>();

        return key;
    }

    public async Task SetClientKey(string ipAddress, string key)
    {
        await Task.CompletedTask;

        if (!File.Exists(clientKeyStoreJsonFilePath))
        {
            File.WriteAllText(clientKeyStoreJsonFilePath, DefaultJsonContents);
        }

        var clientKeyStoreFileContents = File.ReadAllText(clientKeyStoreJsonFilePath);

        if (string.IsNullOrWhiteSpace(clientKeyStoreFileContents))
        {
            clientKeyStoreFileContents = DefaultJsonContents;
        }

        var json = JObject.Parse(clientKeyStoreFileContents);

        json[ipAddress] = new JObject
        {
            [ClientKeyJsonPropertyName] = key
        };

        File.WriteAllText(clientKeyStoreJsonFilePath, json.ToString());
    }
}
