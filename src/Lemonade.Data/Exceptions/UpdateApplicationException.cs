using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateApplicationException : Exception
    {
        public UpdateApplicationException(Exception innerException)
            : base(string.Format(Errors.FailedToUpdateApplication), innerException)
        {
        }
    }
}