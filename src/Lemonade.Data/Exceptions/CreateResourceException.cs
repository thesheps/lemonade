using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateResourceException : Exception
    {
        public CreateResourceException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateResource), innerException)
        {
        }
    }
}