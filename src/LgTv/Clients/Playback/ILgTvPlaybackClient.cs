﻿using System.Threading.Tasks;

namespace LgTv.Clients.Playback
{
    public interface ILgTvPlaybackClient
    {
        Task Play();

        Task Pause();

        Task Stop();

        Task FastForward();

        Task Rewind();
    }
}
