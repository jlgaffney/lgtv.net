﻿namespace LgTv.Features.Audio;

public interface IAudioClient
{
    Task<string> GetOutput();

    Task<int> GetVolume();

    Task<bool> IsMuted();

    Task VolumeUp();

    Task VolumeDown();

    Task SetVolume(int value);

    Task SetMute(bool value);

    Task ToggleMute();
}
