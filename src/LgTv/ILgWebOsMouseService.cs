using System;
using System.Threading.Tasks;

namespace LgTv
{
    public interface ILgWebOsMouseService : IDisposable
    {
        Task<bool> Connect(string uri);

        Task SendButton(int number);

        Task SendButton(ButtonType bt);


        Task Move(double dx, double dy, bool drag = false);

        Task Scroll(double dx, double dy);

        Task Click();
    }
}
