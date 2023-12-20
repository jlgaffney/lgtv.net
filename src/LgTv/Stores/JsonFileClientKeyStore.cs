using Newtonsoft.Json.Linq;

namespace LgTv.Stores;

public class JsonFileClientKeyStore(string clientKeyStoreJsonFilePath) : IClientKeyStore
{
    private const string DefaultJsonContents = "{\n}";
    private const string ClientKeyJsonPropertyName = "client-key";

    private readonly string _clientKeyStoreJsonFilePath = clientKeyStoreJsonFilePath ?? throw new ArgumentNullException(nameof(clientKeyStoreJsonFilePath));

    public async Task<string> GetClientKey(string ipAddress)
    {
        if (!File.Exists(_clientKeyStoreJsonFilePath))
        {
            return null;
        }

        var clientKeyStoreFileContents = await File.ReadAllTextAsync(_clientKeyStoreJsonFilePath);

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
        if (!File.Exists(_clientKeyStoreJsonFilePath))
        {
            await File.WriteAllTextAsync(_clientKeyStoreJsonFilePath, DefaultJsonContents);
        }

        var clientKeyStoreFileContents = await File.ReadAllTextAsync(_clientKeyStoreJsonFilePath);

        if (string.IsNullOrWhiteSpace(clientKeyStoreFileContents))
        {
            clientKeyStoreFileContents = DefaultJsonContents;
        }

        var json = JObject.Parse(clientKeyStoreFileContents);

        json[ipAddress] = new JObject
        {
            [ClientKeyJsonPropertyName] = key
        };

        await File.WriteAllTextAsync(_clientKeyStoreJsonFilePath, json.ToString());
    }
}
