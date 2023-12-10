using LgTv.Connections;

namespace LgTv.Clients.Audio;

internal class LgTvAudioClient(ILgTvConnection connection) : ILgTvAudioClient
{
    public async Task<string> GetOutput()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetSoundOutput.Prefix, LgTvCommands.GetSoundOutput.Uri);
        var response = await connection.SendCommandAsync(requestMessage);
        return response.soundOutput;
    }

    public async Task<int> GetVolume()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetVolume.Prefix, LgTvCommands.GetVolume.Uri);
        var response = await connection.SendCommandAsync(requestMessage);
        return int.Parse((string) response.volumeStatus.volume);
    }

    public async Task<bool> IsMuted()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetVolumeIsMuted.Prefix, LgTvCommands.GetVolumeIsMuted.Uri);
        var response = await connection.SendCommandAsync(requestMessage);
        return (bool) response.mute;
    }

    public async Task VolumeUp()
    {
        var requestMessage = new RequestMessage(LgTvCommands.SetVolumeUp.Prefix, LgTvCommands.SetVolumeUp.Uri);
        await connection.SendCommandAsync(requestMessage);
    }

    public async Task VolumeDown()
    {
        var requestMessage = new RequestMessage(LgTvCommands.SetVolumeDown.Prefix, LgTvCommands.SetVolumeDown.Uri);
        await connection.SendCommandAsync(requestMessage);
    }

    public async Task SetVolume(int value)
    {
        if (value < 0)
        {
            value = 0;
        }

        if (value > 100)
        {
            value = 100;
        }

        var requestMessage = new RequestMessage(LgTvCommands.SetVolume.Uri, new { volume = value });
        await connection.SendCommandAsync(requestMessage);
    }

    public async Task SetMute(bool value)
    {
        var requestMessage = new RequestMessage(LgTvCommands.SetMute.Uri, new { mute = value });
        await connection.SendCommandAsync(requestMessage);
    }

    public async Task ToggleMute()
    {
        await SetMute(!await IsMuted());
    }
}
