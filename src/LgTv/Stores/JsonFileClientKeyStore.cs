using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LgTv
{
    public class JsonFileClientKeyStore : IClientKeyStore
    {
        private const string DefaultJsonContents = "{\n}";
        private const string ClientKeyJsonPropertyName = "client-key";

        private readonly string _clientKeyStoreJsonFilePath;

        public JsonFileClientKeyStore(
            string clientKeyStoreJsonFilePath)
        {
            _clientKeyStoreJsonFilePath = clientKeyStoreJsonFilePath;
        }

        public async Task<string> GetClientKey(string ipAddress)
        {
            await Task.CompletedTask;

            if (!File.Exists(_clientKeyStoreJsonFilePath))
            {
                return null;
            }

            var clientKeyStoreFileContents = File.ReadAllText(_clientKeyStoreJsonFilePath);

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

            if (!File.Exists(_clientKeyStoreJsonFilePath))
            {
                File.WriteAllText(_clientKeyStoreJsonFilePath, DefaultJsonContents);
            }

            var clientKeyStoreFileContents = File.ReadAllText(_clientKeyStoreJsonFilePath);

            if (string.IsNullOrWhiteSpace(clientKeyStoreFileContents))
            {
                clientKeyStoreFileContents = DefaultJsonContents;
            }

            var json = JObject.Parse(clientKeyStoreFileContents);

            json[ipAddress] = new JObject
            {
                [ClientKeyJsonPropertyName] = key
            };

            File.WriteAllText(_clientKeyStoreJsonFilePath, json.ToString());
        }
    }
}
