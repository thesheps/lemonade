using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateResourceException : Exception
    {
        public CreateResourceException(Exception innerException)
            : base(Errors.FailedToCreateResource, innerException)
        {
        }
    }
}