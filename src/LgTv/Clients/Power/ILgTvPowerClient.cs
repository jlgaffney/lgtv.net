using System.Threading.Tasks;

namespace LgTv.Clients.Power
{
    public interface ILgTvPowerClient
    {
        /// <remarks>Not supported on browser</remarks>
        Task TurnOn();

        Task TurnOff();
    }
}
