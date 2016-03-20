using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateFeatureException : Exception
    {
        public CreateFeatureException(Exception innerException)
            : base(Errors.FailedToCreateFeature, innerException)
        {
        }
    }
}