using System;

namespace Lemonade.Core.Exceptions
{
    public class UpdateApplicationException : Exception
    {
        public UpdateApplicationException(Exception innerException)
            : base(string.Format(Errors.FailedToUpdateApplication), innerException)
        {
        }
    }
}