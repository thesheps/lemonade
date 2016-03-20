using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateApplicationException : Exception
    {
        public CreateApplicationException(Exception innerException)
            : base(Errors.FailedToCreateApplication, innerException)
        {
        }
    }
}