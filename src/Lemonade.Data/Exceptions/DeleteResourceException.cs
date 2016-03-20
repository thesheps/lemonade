using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteResourceException : Exception
    {
        public DeleteResourceException(Exception innerException)
            : base(Errors.FailedToDeleteResource, innerException)
        {
        }
    }
}