using System.Threading.Tasks;

namespace LgTv.Power
{
    public interface ILgTvPowerClient
    {
        /// <remarks>Not supported on browser</remarks>
        Task TurnOn();

        Task TurnOff();
    }
}
