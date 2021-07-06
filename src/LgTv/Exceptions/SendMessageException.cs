using System;

namespace LgTv.Exceptions
{
    public class SendMessageException : Exception
    {
        public SendMessageException(string message, Exception e)
            : base(message, e)
        {

        }
    }
}
