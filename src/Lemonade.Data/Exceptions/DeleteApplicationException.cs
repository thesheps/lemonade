using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteApplicationException : Exception
    {
        public DeleteApplicationException(Exception innerException)
            : base(string.Format(Errors.FailedToDeleteApplication), innerException)
        {
        }
    }
}