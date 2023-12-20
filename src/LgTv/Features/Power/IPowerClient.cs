namespace LgTv.Features.Power;

public interface IPowerClient
{
    /// <remarks>Not supported on browser</remarks>
    Task TurnOn();

    Task TurnOff();
}
