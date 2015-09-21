using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteApplicationException : Exception
    {
        public DeleteApplicationException(string applicationName, Exception innerException)
            : base(string.Format(Errors.FailedToDeleteApplication, applicationName, innerException.Message), innerException)
        {
        }
    }
}