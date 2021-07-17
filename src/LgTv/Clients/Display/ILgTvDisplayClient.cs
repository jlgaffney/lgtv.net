using System.Threading.Tasks;

namespace LgTv.Display
{
    public interface ILgTvDisplayClient
    {
        Task TurnOn3D();

        Task TurnOff3D();

        Task<bool> IsTurnedOn3D();
    }
}
