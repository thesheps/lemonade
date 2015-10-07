using System;

namespace Lemonade.Core.Exceptions
{
    public class CreateFeatureException : Exception
    {
        public CreateFeatureException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateFeature), innerException)
        {
        }
    }
}