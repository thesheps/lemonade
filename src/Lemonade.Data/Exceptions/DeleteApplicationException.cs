using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteApplicationException : Exception
    {
        public DeleteApplicationException(Exception innerException)
            : base(Errors.FailedToDeleteApplication, innerException)
        {
        }
    }
}