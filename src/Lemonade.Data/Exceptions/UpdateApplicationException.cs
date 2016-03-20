using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateApplicationException : Exception
    {
        public UpdateApplicationException(Exception innerException)
            : base(Errors.FailedToUpdateApplication, innerException)
        {
        }
    }
}