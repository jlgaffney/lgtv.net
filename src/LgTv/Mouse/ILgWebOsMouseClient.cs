using System;
using System.Threading.Tasks;

namespace LgTv.Mouse
{
    public enum ButtonType
    {
        HOME,
        BACK,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        RED,
        BLUE,
        YELLOW,
        GREEN
    }

    public interface ILgWebOsMouseClient : IDisposable
    {
        Task<bool> Connect(string uri);

        Task SendButton(int number);

        Task SendButton(ButtonType bt);


        Task Move(double dx, double dy, bool drag = false);

        Task Scroll(double dx, double dy);

        Task Click();
    }
}
