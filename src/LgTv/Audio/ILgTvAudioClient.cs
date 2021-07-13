﻿using System.Threading.Tasks;

namespace LgTv.Audio
{
    public interface ILgTvAudioClient
    {
        Task<int> GetVolume();

        Task<bool> IsMuted();

        Task VolumeUp();

        Task VolumeDown();

        Task SetVolume(int value);

        Task SetMute(bool value);

        Task ToggleMute();
    }
}
