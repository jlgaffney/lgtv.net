using System.Threading.Tasks;

namespace LgTv.Playback
{
    public interface ILgTvPlaybackClient
    {
        Task Play();

        Task Pause();

        Task Stop();
    }
}
