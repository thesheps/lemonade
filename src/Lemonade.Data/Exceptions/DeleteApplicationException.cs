using System;

namespace Lemonade.Core.Exceptions
{
    public class DeleteApplicationException : Exception
    {
        public DeleteApplicationException(Exception innerException)
            : base(string.Format(Errors.FailedToDeleteApplication), innerException)
        {
        }
    }
}