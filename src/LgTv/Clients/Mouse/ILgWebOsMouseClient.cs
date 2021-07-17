using System;
using System.Threading.Tasks;

namespace LgTv.Mouse
{
    public interface ILgWebOsMouseClient : IDisposable
    {
        Task<bool> Connect(string uri);

        Task SendButton(int number);

        Task SendButton(string name);


        Task Move(double dx, double dy, bool drag = false);

        Task Scroll(double dx, double dy);

        Task Click();
    }
}
