using LgTv.Interaction;
using LgTv.Networking;
using LgTv.Stores;

namespace LgTv.Sample.Console;

public class Program
{
    private const string ClientKeyStoreFileName = "client-keys.json";

    private static readonly string ClientKeyStoreFilePath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), ClientKeyStoreFileName);

    private const string TvHost = "ENTER_HOST";
    private const int TvPort = Client.DefaultPort;

    public static async Task Main()
    {
        // Initialization
        var client = new Client(
            new IPAddressResolver(),
            new MacAddressResolver(),
            new WakeOnLan(),
            () => new Connection(),
            new JsonFileClientKeyStore(ClientKeyStoreFilePath),
            TvHost, TvPort);

        await client.Power.TurnOn();

        await client.Connect();
        await client.MakeHandShake();


        var systemInfo = await client.Info.GetSystemInfo();
        var softwareInfo = await client.Info.GetSoftwareInfo();
        var connectionInfo = await client.Info.GetConnectionInfo();


        // Volume control
        await client.Audio.VolumeDown();
        await client.Audio.VolumeUp();


        // Playback control
        await client.Playback.Pause();
        await client.Playback.Play();


        var channels = await client.Channels.GetChannels();

        var inputs = await client.Inputs.GetInputs();

        var apps = await client.Apps.GetApps();


        await using (var mouse = await client.Mouse)
        {
            await mouse.SendButton(ButtonType.Up);
            await mouse.SendButton(ButtonType.Left);
            await mouse.SendButton(ButtonType.Right);
            await mouse.SendButton(ButtonType.Down);
        }


        await client.Power.TurnOff();
    }
}
