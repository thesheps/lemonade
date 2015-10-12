using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateFeatureException : Exception
    {
        public CreateFeatureException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateFeature), innerException)
        {
        }
    }
}