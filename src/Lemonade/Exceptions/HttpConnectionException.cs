using System;

namespace Lemonade.Exceptions
{
    public class HttpConnectionException : Exception
    {
        public HttpConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}