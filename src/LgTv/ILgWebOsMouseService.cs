using System;
using System.Threading.Tasks;

namespace LgTv
{
    public interface ILgWebOsMouseService : IDisposable
    {
        Task<bool> Connect(string uri);

        void SendButton(int number);

        void SendButton(ButtonType bt);


        void Move(double dx, double dy, bool drag = false);

        void Scroll(double dx, double dy);

        void Click();
    }
}
