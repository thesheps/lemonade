using System;

namespace Lemonade.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}