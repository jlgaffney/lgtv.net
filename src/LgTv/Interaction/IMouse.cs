namespace LgTv.Interaction;

public interface IMouse : IAsyncDisposable
{
    Task<bool> Connect(string uri);

    Task SendButton(int number);

    /// <summary>
    /// See <see cref="ButtonType" />
    /// </summary>
    Task SendButton(string name);


    Task Move(double dx, double dy, bool drag = false);

    Task Scroll(double dx, double dy);

    Task Click();
}
