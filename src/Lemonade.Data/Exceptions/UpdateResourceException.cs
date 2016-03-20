using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateResourceException : Exception
    {
        public UpdateResourceException(Exception innerException)
            : base(Errors.FailedToCreateFeature, innerException)
        {
        }
    }
}