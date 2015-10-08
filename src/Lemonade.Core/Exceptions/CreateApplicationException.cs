using System;

namespace Lemonade.Core.Exceptions
{
    public class CreateApplicationException : Exception
    {
        public CreateApplicationException(Exception innerException)
            : base(string.Format(Errors.FailedToCreatedApplication), innerException)
        {
        }
    }
}