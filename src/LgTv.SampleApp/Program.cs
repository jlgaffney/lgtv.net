using System.IO;
using System.Threading.Tasks;

namespace LgTv.SampleApp
{
    public class Program
    {
        private const string ClientKeyStoreFileName = "client-keys.json";

        private static readonly string ClientKeyStoreFilePath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), ClientKeyStoreFileName);

        private const string TvHostname = "192.168.1.196";
        private const int TvPort = 3000;

        public static async Task Main(string[] args)
        {
            // Initalization
            var client = new LgTvClient(new LgTvConnection(), new JsonFileClientKeyStore(ClientKeyStoreFilePath, TvHostname), TvHostname, TvPort);
            
            await client.Connect();
            await client.MakeHandShake();

            var mouse = await client.GetMouse();
            
            //control
            await client.VolumeDown();
            await client.VolumeUp();

            await client.Pause();
            await client.Play();

            var apps = await client.GetApps();
            
            
            (await client.GetMouse()).SendButton(ButtonType.BACK);
            (await client.GetMouse()).SendButton(ButtonType.UP);
            (await client.GetMouse()).SendButton(ButtonType.LEFT);


            await client.TurnOff();
        }
    }
}
