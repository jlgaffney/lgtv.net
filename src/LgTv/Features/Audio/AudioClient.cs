namespace LgTv.Features.Audio;

internal class AudioClient(IConnection connection) : IAudioClient
{
    private readonly IConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public async Task<string> GetOutput()
    {
        var requestMessage = new RequestMessage(Commands.GetSoundOutput.Prefix, Commands.GetSoundOutput.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);
        return response.soundOutput;
    }

    public async Task<int> GetVolume()
    {
        var requestMessage = new RequestMessage(Commands.GetAudioStatus.Prefix, Commands.GetAudioStatus.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);
        return int.Parse((string)response.volume);
    }

    public async Task<bool> IsMuted()
    {
        var requestMessage = new RequestMessage(Commands.GetAudioStatus.Prefix, Commands.GetAudioStatus.Uri);
        var response = await _connection.SendCommandAsync(requestMessage);
        return (bool)response.mute;
    }

    public async Task VolumeUp()
    {
        var requestMessage = new RequestMessage(Commands.SetVolumeUp.Prefix, Commands.SetVolumeUp.Uri);
        await _connection.SendCommandAsync(requestMessage);
    }

    public async Task VolumeDown()
    {
        var requestMessage = new RequestMessage(Commands.SetVolumeDown.Prefix, Commands.SetVolumeDown.Uri);
        await _connection.SendCommandAsync(requestMessage);
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

        var requestMessage = new RequestMessage(Commands.SetVolume.Uri, new { volume = value });
        await _connection.SendCommandAsync(requestMessage);
    }

    public async Task SetMute(bool value)
    {
        var requestMessage = new RequestMessage(Commands.SetMute.Uri, new { mute = value });
        await _connection.SendCommandAsync(requestMessage);
    }

    public async Task ToggleMute()
    {
        await SetMute(!await IsMuted());
    }
}
