using System.Threading.Tasks;

namespace LgTv.Clients.Display
{
    public interface ILgTvDisplayClient
    {
        Task TurnOn3D();

        Task TurnOff3D();

        Task<bool> Is3DTurnedOn();
    }
}
